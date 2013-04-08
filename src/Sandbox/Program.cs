using Alchemy;
using Alchemy.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Sandbox {
    class Program {
        static void Main(string[] args) {
            Alchemy();
        }

        static void Alchemy() {
            var socket = new WebSocketClient("ws://websocket.mtgox.com:80/mtgox?currency=USD") {
                OnConnect = OnConnect,
                OnConnected = OnConnected,
                OnDisconnect = OnDisconnect,
                OnReceive = OnReceive,
                OnSend = OnSend,
            };

            socket.Connect();
            //Console.WriteLine("{0} {1}", socket.Connected, socket.ReadyState);

            (new AutoResetEvent(false)).WaitOne();
        }

        static void OnConnect(UserContext context) {
            Console.WriteLine("OnConnect");
        }

        static void OnConnected(UserContext context) {
            Console.WriteLine("OnConnecting");
        }

        static void OnDisconnect(UserContext context) {
            Console.WriteLine("OnDisconnect");

        }

        static void OnReceive(UserContext context){
            Console.WriteLine("OnReceive");
        }

        static void OnSend(UserContext context) {
            Console.WriteLine("OnSend");
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


