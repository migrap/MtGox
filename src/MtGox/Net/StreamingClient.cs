using MtGox.Net.Http.Formatting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Reactive.Concurrency;
using MtGox.Data;
using System.Reactive.Subjects;

namespace MtGox.Net {
    public class StreamingClient {
        private BlockingCollection<StreamContent> _streams = new BlockingCollection<StreamContent>();
        private MtGoxMediaTypeFormatter _formatter = new MtGoxMediaTypeFormatter();
        private ClientWebSocket _wocket = new ClientWebSocket();
        private Encoding _encoding = Encoding.UTF8;

        private ISubject<Message> _messages = new Subject<Message>();

        public IObservable<Trade> Trades {
            get { return _messages.Select(m => m.Data).OfType<Trade>(); }
        }

        public IObservable<Depth> Depths {
            get { return _messages.Select(m => m.Data).OfType<Depth>(); }
        }

        public IObservable<Ticker> Tickers {
            get { return _messages.Select(m => m.Data).OfType<Ticker>(); }
        }

        public StreamingClient() {
            _streams.GetConsumingEnumerable()
                .ToObservable(Scheduler.Default)
                .Subscribe(OnStreamContent);
        }

        public async Task ConnectAsync(string uri) {
            await ConnectAsync(new Uri(uri));
        }

        public async Task ConnectAsync(Uri uri) {
            await _wocket.ConnectAsync(uri, CancellationToken.None);

            while (_wocket.State == WebSocketState.Open) {
                var buffer = new ArraySegment<byte>(new byte[8096]);
                var result = await _wocket.ReceiveAsync(buffer, CancellationToken.None);
                var content = buffer.Take(result.Count).GetStreamContent();
                _streams.Add(content);
            }
        }

        private void OnStreamContent(StreamContent value) {
            value.ReadAsAsync<Message>(_formatter)
                .ContinueWith(x => _messages.OnNext(x.Result));
        }
    }        
}
