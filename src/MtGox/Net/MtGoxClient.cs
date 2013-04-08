using MtGox.Net.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MtGox.Net {
    public class MtGoxClient {
        private HttpClient _client;
        private HttpMessageHandler _handler;

        public MtGoxClient(string scheme = "https", string host = "data.mtgox.com", int port = 443, string path = "api/2") {
            _handler = new MtGoxDelegatingHandler("", "");
            var builder = new UriBuilder { Scheme = scheme, Host = host, Port = port, Path = path };

            _client = new HttpClient(_handler) {
                BaseAddress = builder.Uri
            };
        }
    }
}
