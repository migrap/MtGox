using MtGox.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtGox.Models {
    public class Trade {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("date")]
        [JsonConverter(typeof(DateTimeOffsetConverter))]
        public DateTimeOffset Datetime { get; set; }

        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("tid")]
        public string Tid { get; set; }

        [JsonProperty("item")]
        public string Item { get; set; }

        [JsonProperty("price_currency")]
        public string Currency { get; set; }

        [JsonProperty("trade_type")]
        public string At { get; set; }

        [JsonProperty("primary")]
        [JsonConverter(typeof(PrimaryConverter))]
        public bool Primary { get; set; }

        [JsonProperty("properties")]
        public string Properties { get; set; }

        public override string ToString() {
            return (new { Type, Datetime, Amount, Price, Tid, Item, Currency, At, Primary, Properties }).ToString();
        }
    }
}