using MtGox.Data;
using MtGox.Models.Ticker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MtGox {
    public partial class MtGoxClient {
        public async Task<TickerResponse> GetTickerAsync(string symbol) {
            return await GetAsync("BTC{0}/money/ticker".FormatWith(symbol))
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult()
                .Content.ReadAsAsync<TickerResponse>(_formatter);
        }
    }
}
