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
using System.Net.Http;
using System.Collections.Specialized;
using System.Web;
using System.Security.Cryptography;

namespace Sandbox {
    class Program {
        static ClientWebSocket _ws = new ClientWebSocket();

        static void Main(string[] args) {
            var client = MtGoxClient.New(x => {
                x.Key("");
                x.Secret("");
            });

            var idkey = client.GetIdkeyAsync().Result;

        }
    }
}