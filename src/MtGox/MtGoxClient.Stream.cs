using MtGox.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Reactive.Subjects;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using System.Reactive.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using MtGox.Json.Converters;
using MtGox.Configuration;

namespace MtGox {
    public partial class MtGoxClient {
        private Lazy<JsonSerializerSettings> _settings = new Lazy<JsonSerializerSettings>(() => {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new PrimaryConverter());
            settings.Converters.Add(new MessageConverter());
            settings.Converters.Add(new DateTimeOffsetConverter());
            return settings;
        });
        private WebSocket _wocket;        
        private ISubject<Message> _messages = new Subject<Message>();        

        private JsonSerializerSettings Settings {
            get { return _settings.Value; }
        }
        public async Task<bool> ConnectAsync(string address = "wss://websocket.mtgox.com") {
            var tcs = new TaskCompletionSource<bool>();
            var opened = (EventHandler)null;
            var closed = (EventHandler<CloseEventArgs>)null;
            var error = (EventHandler<ErrorEventArgs>)null;
            var message = (EventHandler<MessageEventArgs>)null;
            var validate = (RemoteCertificateValidationCallback)null;

            _wocket = new WebSocket(address);
            _wocket.Origin = "{0}:{1}".FormatWith(_wocket.Url.Host, _wocket.Url.Scheme == "wss" ? "443" : "80");

            _wocket.OnOpen += opened = (s, e) => {
                _wocket.OnOpen -= opened;
                tcs.SetResult(true);
            };

            _wocket.OnError += error = (s, e) => {
                Trace.WriteLine(e.Message);
            };

            _wocket.OnMessage += message = (s, e) => {
                var jobject = JObject.Parse(e.Data);
                if(jobject.FieldExists("channel")) {
                    var m = JsonConvert.DeserializeObject<Message>(e.Data, Settings);
                    _messages.OnNext(m);
                } else {
                    Trace.WriteLine(e.Data);
                }
            };

            _wocket.ServerCertificateValidationCallback = validate = (sender, certificate, chain, sslPolicyErrors) => {
                return (sslPolicyErrors == SslPolicyErrors.None);
            };

            _wocket.OnClose += closed = (s, e) => {
                _wocket.OnClose -= closed;
                _wocket.OnError -= error;
                _wocket.OnMessage -= message;
                _wocket.ServerCertificateValidationCallback = null;
            };

            _wocket.Connect();

            return await tcs.Task;
        }

        internal IObservable<Message> Messages { get { return _messages; } }
        
        internal void Subscribe(string value) {
            var json = JsonConvert.SerializeObject(new { op = "mtgox.subscribe", channel = value });
            _wocket.Send(json);
        }

        public IObservable<Trade> Trade(Action<ITradeConfigurator> configure) {
            var c = new TradeConfigurator(this);
            configure(c);
            return c.Build();
        }

        public IObservable<Ticker> Ticker(Action<ITickerConfigurator> configure) {
            var c = new TickerConfigurator(this);
            configure(c);
            return c.Build();
        }

        public IObservable<Depth> Depth(Action<IDepthConfigurator> configure) {
            var c = new DepthConfigurator(this);
            configure(c);
            return c.Build();
        }
    }
}