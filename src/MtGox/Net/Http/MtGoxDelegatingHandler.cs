using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace MtGox.Net.Http {
    internal class MtGoxDelegatingHandler : DelegatingHandler {
        private SecureString _key;
        private SecureString _secret;
        private string _useragent;

        public MtGoxDelegatingHandler(string key, string secret)
            : this(key.GetSecureString(), secret.GetSecureString()) {
        }

        public MtGoxDelegatingHandler(SecureString key, SecureString secret) {
            _key = key;
            _secret = secret;
            _useragent = "{0} {1}".FormatWith(typeof(MtGoxDelegatingHandler).Assembly.GetName().Name, typeof(MtGoxDelegatingHandler).Assembly.GetName().Version.ToString(4));

            InnerHandler = new HttpClientHandler {
                AllowAutoRedirect = false,
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip | DecompressionMethods.None,
            };

            ServicePointManager.Expect100Continue = false;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
            var nonce = DateTime.Now.Ticks;
            var key = _key.GetString();

            var post = "{0}={1}".FormatWith("nonce", nonce);
            var query = request.RequestUri.ParseQueryString();
            var prefix = request.RequestUri.Segments.Reverse().Take(2).Reverse().Join("");

            if (null != request.RequestUri.Query && request.RequestUri.Query.IsNotNullOrEmpty()) {
                post = "{0}{1}".FormatWith(post, request.RequestUri.Query);
            }

            var signature = "{0}{1}{2}".FormatWith(prefix, Convert.ToChar(0), post);

            var sign = GetHash(_secret, signature, Encoding.UTF8);

            request.Headers.Add("Rest-Key", key);
            request.Headers.Add("Rest-Sign", sign);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", _useragent);

            if (request.Method == HttpMethod.Post) {                
                request.Content = new FormUrlEncodedContent(new Dictionary<string, string> {
                    {"nonce",nonce.ToString()}
                });
            }

            return base.SendAsync(request, cancellationToken);
        }

        private static string GetHash(SecureString key, string value, Encoding encoding) {
            return GetHash(key.GetString(), value, encoding);
        }

        private static string GetHash(string key, string value, Encoding encoding) {
            var hash = new HMACSHA512(Convert.FromBase64String(key));
            var buffer = encoding.GetBytes(value);
            return Convert.ToBase64String(hash.ComputeHash(buffer));
        }
    }

    internal static partial class Extensions {
        public static bool IsNullOrEmpty(this string self) {
            return string.IsNullOrEmpty(self);
        }

        public static bool IsNotNullOrEmpty(this string self) {
            return !string.IsNullOrEmpty(self);
        }

        internal static string Join(this IEnumerable<object> source, string seperator) {
            return string.Join(seperator, source);
        }
    }
}