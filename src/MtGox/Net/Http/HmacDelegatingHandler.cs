using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MtGox.Net.Http {
    internal class HmacDelegatingHandler : DelegatingHandler {
        public static HmacDelegatingHandler New(Action<IHmacDelegatingHandlerConfigurator> configure) {
            var configurator = new HmacDelegatingHandlerConfigurator();
            configure(configurator);
            return configurator.Build();
        }

        private SecureString _secret;
        private Encoding _encoding;
        private Func<object> _nonce;
        private Func<SecureString, string, Encoding, string> _hash;

        internal HmacDelegatingHandler(SecureString secret, Encoding encoding, Func<object> nonce, Func<SecureString, string, Encoding, string> hash) {
            _secret = secret;
            _encoding = encoding;
            _nonce = nonce;
            _hash = hash;

            InnerHandler = new HttpClientHandler {
                AllowAutoRedirect = true,
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip | DecompressionMethods.None,
            };
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
            var nonce = _nonce();
            var hash = _hash;
            var secret = _secret;
            var encoding = _encoding;

            var post = "{0}={1}".FormatWith("nonce", nonce);
            var query = request.RequestUri.ParseQueryString();
            var prefix = request.RequestUri.Segments.Reverse().Take(2).Reverse().Join("");

            //if (null != request.RequestUri.Query && request.RequestUri.Query.IsNotNullOrEmpty()) {
            //    post = "{0}{1}".FormatWith(post, request.RequestUri.Query);
            //}

            var signature = "{0}{1}{2}".FormatWith(prefix, Convert.ToChar(0), post);
            var sign = hash(secret, signature, encoding);

            request.Headers.Add("Rest-Sign", sign);

            if (request.Method == HttpMethod.Post) {
                request.Content = new FormUrlEncodedContent(new Dictionary<string, string> {
                    {"nonce",nonce.ToString()}
                });
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
