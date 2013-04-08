using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtGox.Data {
    public class Trade {
        [JsonProperty(PropertyName = "amount")]
        public double Amount { get; set; }

        [JsonProperty(PropertyName = "date")]
        public DateTime DateTime { get; set; }

        [JsonProperty(PropertyName = "item")]
        public string Item { get; set; }

        [JsonProperty(PropertyName = "price")]
        public double Price { get; set; }

        [JsonProperty(PropertyName = "price_currency")]
        public string Currency { get; set; }

        [JsonProperty(PropertyName = "primary")]
        public string Primary { get; set; }

        [JsonProperty(PropertyName = "properties")]
        public string Properties { get; set; }

        [JsonProperty(PropertyName = "tid")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "trade_type")]
        public Market At { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }
}
