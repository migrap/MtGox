using MtGox.Configuration;
using MtGox.Net.Http;
using MtGox.Net.Http.Formatting;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security;
using System.Threading.Tasks;

namespace MtGox {
    public partial class MtGoxClient {
        private MtGoxMediaTypeFormatter _formatter = new MtGoxMediaTypeFormatter();
        private HttpClient _http;
        private SecureString _key;
        private SecureString _secret;

        public static MtGoxClient New(Action<IMtGoxClientConfigurator> configure) {
            var configurator = new MtGoxClientConfigurator();
            configure(configurator);
            return configurator.Build();
        }

        public MtGoxClient(string scheme = "https", string host = "data.mtgox.com", int port = 443, string path = "api", string key = "", string secret = "") {
            var builder = new UriBuilder { Scheme = scheme, Host = host, Port = port, Path = path };
            var handler = HmacDelegatingHandler.New(x=>{
                x.Secret(secret);
            });

            _http = new HttpClient(handler);
            _http.BaseAddress = builder.Uri;
            _http.DefaultRequestHeaders.Add("User-Agent", "{0} {1}".FormatWith(typeof(MtGoxClient).Assembly.GetName().Name, typeof(MtGoxClient).Assembly.GetName().Version.ToString(4)));
            _http.DefaultRequestHeaders.Add("Rest-Key", key);
            _http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _key = key.GetSecureString();
            _secret = key.GetSecureString();
        }

        private async Task<HttpResponseMessage> SendAsync(HttpMethod method, Action<IHttpRequestMessageConfigurator> configure) {
            var configurator = new HttpRequestMessageConfigurator();
            
            configurator.Method(method);
            configurator.BaseAddress(_http.BaseAddress);
            configure(configurator);

            var request = configurator.Build();

            var response = await _http.SendAsync(request).ConfigureAwait(false);
            await response.EnsureSuccessStatusCode(true).ConfigureAwait(false);
            return response;
        }

        private async Task<HttpResponseMessage> GetAsync(string path, object values = null) {
            return await SendAsync(HttpMethod.Get, x => {
                x.Path(path);
                x.Values(values);
            });
        }

        private async Task<HttpResponseMessage> PostAsync(string path) {
            return await SendAsync(HttpMethod.Post, x => {
                x.Path(path);
            });
        }
    }
}

////public async Task ConnectAsync(string uri) {
////    await _streaming.ConnectAsync(uri);
////}        

//public IObservable<Trade> Trades {
//    get { return _streaming.Trades; }
//}

//internal Task<TResult> SendAsync<TValue, TResult>(HttpRequestMessage request, TValue value) {
//    request.Content = new ObjectContent<TValue>(value, _formatter);

//    return SendAsync(request)
//        .ContinueWith(x => x.Result.Content.ReadAsAsync<TResult>(_formatter))
//        .Unwrap();
//}

//internal Task<HttpResponseMessage> SendAsync(HttpRequestMessage request) {
//    request.RequestUri = _http.BaseAddress.Append(request.RequestUri.OriginalString);
//    return _http.SendAsync(request);        
//}

//internal Task<T> GetAsync<T>(string path, object values = null) {
//    var request = new HttpRequestMessage(HttpMethod.Get, (null == values) ? path : "{0}?{1}".FormatWith(path, values.ToQueryString()));
//    return SendAsync(request).ContinueWith(x => x.Result.Content.ReadAsAsync<T>(_formatter)).Unwrap();
//}

////internal Task<T> PostAsync<T>(string path, object values = null) {
////    var request = new HttpRequestMessage(HttpMethod.Post,
////        (null == values) ? path : "{0}?{1}".FormatWith(path, values.ToQueryString()));

////    return SendAsync(request)
////        .ContinueWith(x => x.Result.Content.ReadAsAsync<T>(_formatter))
////        .Unwrap();
////}

////public async Task<Idkey> GetIdkeyAsync() {
////    return await PostAsync<Idkey>("money/idkey");
////}

////public async Task<InfoResponse> GetInfoAsync() {
////    return await PostAsync<InfoResponse>("money/info");
////}

//public async Task<Ticker> GetTickerAsync(string symbol) {
//    return await GetAsync<TickerResponse>("BTC{0}/money/ticker".FormatWith(symbol)).ContinueWith(x => x.Result.ToTicker());
//}

////public async Task GetQuoteAsync(string symbol) {
////}