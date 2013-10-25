using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Specialized;
using System.Web;
using System.Security.Cryptography;
using WebSocketSharp;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Net;
using System.IO;
using MtGox.Models;


namespace Sandbox {
    class Program {
        static void Main(string[] args) {
            var client = new MtGox.MtGoxClient();
            client.ConnectAsync().Wait();
            client.Trade.Subscribe(x => Console.WriteLine(x));

            Console.ReadLine();
        }
    }
}