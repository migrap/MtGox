using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtGox.Models.Money {
    public sealed class TickerResponse {
        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("data")]
        public Atad Data { get; set; }

        public sealed class Atad {
            [JsonProperty("high")]
            public Field High { get; set; }

            [JsonProperty("low")]
            public Field Low { get; set; }

            [JsonProperty("avg")]
            public Field Average { get; set; }

            [JsonProperty("vwap")]
            public Field Vwap { get; set; }

            [JsonProperty("vol")]
            public Field Volume { get; set; }

            [JsonProperty("last")]
            public Field Last { get; set; }

            [JsonProperty("buy")]
            public Field Buy { get; set; }

            [JsonProperty("sell")]
            public Field Sell { get; set; }

            [JsonProperty("item")]
            public string Item { get; set; }

            [JsonProperty("now")]
            public DateTimeOffset Now { get; set; }
        }

        public sealed class Field {
            [JsonProperty("currency")]
            public string Currency { get; set; }

            [JsonProperty("display")]
            public string Display { get; set; }

            [JsonProperty("value")]
            public double Value { get; set; }
        }
    }
}
