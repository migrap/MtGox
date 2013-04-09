using MtGox.Configuration;
using MtGox.Data;
using MtGox.Net.Http;
using MtGox.Net.Http.Formatting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MtGox.Net {
    public class MtGoxClient {
        private MtGoxMediaTypeFormatter _formatter = new MtGoxMediaTypeFormatter();
        private HttpClient _http;
        private HttpMessageHandler _handler;
        private SecureString _key;
        private SecureString _secret;

        public MtGoxClient(string scheme = "https", string host = "data.mtgox.com", int port = 443, string path = "api/2/money", string key = "", string secret = "") {
            _handler = new MtGoxDelegatingHandler(key, secret);
            var builder = new UriBuilder { Scheme = scheme, Host = host, Port = port, Path = path };

            _http = new HttpClient(_handler) {
                BaseAddress = builder.Uri
            };

            _key = key.GetSecureString();
            _secret = key.GetSecureString();
        }

        public static MtGoxClient New(Action<IMtGoxClientConfigurator> configure) {
            var configurator = new MtGoxClientConfigurator();
            configure(configurator);
            return configurator.Build();
        }

        internal Task<T> SendAsync<T>(HttpRequestMessage request) {
            return SendAsync(request)
                .ContinueWith(x => x.Result.Content.ReadAsAsync<T>(_formatter))
                .Unwrap();
        }

        internal Task<TResult> SendAsync<TValue, TResult>(HttpRequestMessage request, TValue value) {
            request.Content = new ObjectContent<TValue>(value, _formatter);

            return SendAsync(request)
                .ContinueWith(x => x.Result.Content.ReadAsAsync<TResult>(_formatter))
                .Unwrap();
        }

        internal Task<HttpResponseMessage> SendAsync(HttpRequestMessage request) {
            request.RequestUri = _http.BaseAddress.Append(request.RequestUri.OriginalString);
            return _http.SendAsync(request);
        }

        private Task<T> GetAsync<T>(params object[] segments) {
            var uri = segments.Join("/");
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return SendAsync<T>(request);
        }

        internal Task<T> PostAsync<T>(params object[] segments) {
            var uri = segments.Join("/");            
            var request = new HttpRequestMessage(HttpMethod.Post, uri);

            return SendAsync(request)
                .ContinueWith(x => x.Result.Content.ReadAsAsync<T>(_formatter))
                .Unwrap();
        }

        public async Task<Idkey> GetIdkeyAsync() {
            return await PostAsync<Idkey>("idkey");
        }

        public async Task<HttpResponseMessage> GetInfoAsync() {
            return await PostAsync<HttpResponseMessage>("info");
        }
    }
}
