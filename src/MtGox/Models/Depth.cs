using MtGox.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtGox.Models {
    public sealed class Depth {
        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("type_str")]
        public string Type { get; set; }

        [JsonProperty("volume")]
        public double Volume { get; set; }

        [JsonProperty("item")]
        public string Item { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("now")]
        [JsonConverter(typeof(DateTimeOffsetConverter))]
        public DateTimeOffset Datetime { get; set; }

        public override string ToString() {
            return (new { Datetime, Price, Type, Volume, Item, Currency }).ToString();
        }
    }
}
