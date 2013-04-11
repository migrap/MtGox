using MtGox.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtGox.Models.Money {
    public sealed class TradesResponse {
        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("data")]
        public Atad Data { get; set; }

        public sealed class Atad : List<Trade> {
        }

        public class Trade {
            [JsonProperty("date")]
            public DateTimeOffset DateTime { get; set; }

            [JsonProperty("price")]
            public double Price { get; set; }

            [JsonProperty("amount")]
            public double Amount { get; set; }

            [JsonProperty("tid")]
            public string Tid { get; set; }

            [JsonProperty("price_currency")]
            public string Currency { get; set; }

            [JsonProperty("item")]
            public string Item { get; set; }

            [JsonProperty("trade_type")]
            public Market Type { get; set; }

            [JsonProperty("primary")]
            public bool Primary { get; set; }

            [JsonProperty("properties")]
            public string Properties { get; set; }
        }
    }
}