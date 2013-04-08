using MtGox.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Reactive.Linq;
using MtGox.Net.Http.Formatting;
using MtGox.Data;

namespace Sandbox {
    class Program {
        static ClientWebSocket _ws = new ClientWebSocket();

        static void Main(string[] args) {
            //var json = "{'channel':'24e67e0d-1cad-4cc0-9e7a-f8523ef460fe','channel_name':'depth.BTCUSD','depth':{'currency':'USD','item':'BTC','now':'1365429089969885','price':'189.998','price_int':'18999800','total_volume_int':'1874768389','type':1,'type_str':'ask','volume':'18.74768389','volume_int':'1874768389'},'op':'private','origin':'broadcast','private':'depth'}";
            var json = "{'channel':'dbf1dee9-4f2e-4a08-8cb7-748919a71b21','channel_name':'trade.BTC','op':'private','origin':'broadcast','private':'trade','trade':{'amount':24.6,'amount_int':'2460000000','date':1365429095,'item':'BTC','price':189.97999,'price_currency':'USD','price_int':'18997999','primary':'Y','properties':'limit','tid':'1365429095715516','trade_type':'ask','type':'trade'}}";
            var message = JsonConvert.DeserializeObject<Message>(json, new MessageConverter(), new MarketConverter(), new UnixDateTimeConverter());


            var streaming = new StreamingClient();
            streaming.Trades.Subscribe(x => Console.WriteLine(x));
            streaming.ConnectAsync("ws://websocket.mtgox.com:80/mtgox?currency=USD").Wait();

            (new AutoResetEvent(false)).WaitOne();
        }

        static async Task Connect(string uri) {
            await _ws.ConnectAsync(new Uri(uri), CancellationToken.None);

            while (_ws.State == WebSocketState.Open) {
                var buffer = new ArraySegment<byte>(new byte[8096]);
                var result = await _ws.ReceiveAsync(buffer, CancellationToken.None);
                var message = JsonConvert.DeserializeObject<Message>(Encoding.UTF8.GetString(buffer.Take(result.Count).ToArray()), new MessageConverter());
                //var message = Encoding.UTF8.GetString(buffer.Take(result.Count).ToArray());

                Console.WriteLine("{0} {1}", result.MessageType, message);
            }
        }
    }
}


//OnReceive = OnReceive,
//    OnSend = OnSend,
//    OnConnect = OnConnected,
//    OnConnected = OnConnect,
//    OnDisconnect = OnDisconnect
//  });

//  aClient.Connect();
//  aClient.Send("Hey!"); // string or byte[]
//  aClient.Disconnect();
//}

//static void OnReceive(UserContext context)
//{
//  Console.WriteLine("The server said : " + context.DataFrame.ToString());
//}


