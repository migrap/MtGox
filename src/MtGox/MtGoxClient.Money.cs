using MtGox.Data;
using MtGox.Models;
using MtGox.Models.Money;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MtGox {
    public partial class MtGoxClient {
        public async Task<InfoResponse> GetInfoAsync() {
            return await PostAsync("money/ticker")
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult()
                .Content.ReadAsAsync<InfoResponse>(_formatter)
                .ConfigureAwait(false);
        }

        public async Task<IdkeyResponse> GetIdkeyAsync() {
            return await PostAsync("money/idkey")
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult()
                .Content.ReadAsAsync<IdkeyResponse>(_formatter)
                .ConfigureAwait(false);
        }

        public async Task<OrdersResponse> GetOrdersAsync() {
            return await PostAsync("money/orders")
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult()
                .Content.ReadAsAsync<OrdersResponse>(_formatter)
                .ConfigureAwait(false);
        }

        public async Task<CurrencyResponse> GetCurrencyAsync(string symbol = "USD") {
            return await GetAsync("BTC{0}/money/currency".FormatWith(symbol))
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult()
                .Content.ReadAsAsync<CurrencyResponse>(_formatter)
                .ConfigureAwait(false);
        }

        public async Task<TickerResponse> GetTickerAsync(string symbol = "USD") {
            return await GetAsync("BTC{0}/money/ticker".FormatWith(symbol))
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult()
                .Content.ReadAsAsync<TickerResponse>(_formatter)
                .ConfigureAwait(false);
        }

        public async Task<TradesResponse> GetTradesAsync(DateTimeOffset since) {
            return await GetAsync("BTCUSD/money/trades/fetch", since.ToUnixTime())
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult()
                .Content.ReadAsAsync<TradesResponse>(_formatter)
                .ConfigureAwait(false);
        }

        public async Task<DepthResponse> GetDepthAsync(string symbol = "USD") {
            return await GetAsync("BTC{0}/money/depth".FormatWith(symbol))
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult()
                .Content.ReadAsAsync<DepthResponse>()
                .ConfigureAwait(false);
        }
    }
}