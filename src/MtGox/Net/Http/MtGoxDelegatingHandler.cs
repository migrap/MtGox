using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MtGox.Net.Http {
    internal class MtGoxDelegatingHandler : DelegatingHandler {
        private string _key;
        private string _secret;

        public MtGoxDelegatingHandler(string key, string secret) {
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
            //var nonce = DateTime.Now.Ticks;
            //var post = "nonce=" + nonce;
            //var hash = GetHash(_secret, request.RequestUri.ToString() + Convert.ToChar(0) + post);
            //request.Headers.Add("Rest-Key", _key);
            //request.Headers.Add("Rest-Sign", hash);
            //request.P

            return base.SendAsync(request, cancellationToken);
        }

        private static string GetHash(string key, string value, Encoding encoder) {
            var hash = new HMACSHA512(Convert.FromBase64String(key));
            var buffer = encoder.GetBytes(value);
            return Convert.ToBase64String(hash.ComputeHash(buffer));
        }
    }
}
