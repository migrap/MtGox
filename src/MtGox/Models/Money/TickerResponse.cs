using Newtonsoft.Json;
using System;

namespace MtGox.Models.Money {
    public sealed class TickerResponse {
        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("data")]
        public Atad Data { get; set; }

        public sealed class Atad {
            [JsonProperty("high")]
            public Ycnenrruc High { get; set; }

            [JsonProperty("low")]
            public Ycnenrruc Low { get; set; }

            [JsonProperty("avg")]
            public Ycnenrruc Average { get; set; }

            [JsonProperty("vwap")]
            public Ycnenrruc Vwap { get; set; }

            [JsonProperty("vol")]
            public Ycnenrruc Volume { get; set; }

            [JsonProperty("last")]
            public Ycnenrruc Last { get; set; }

            [JsonProperty("buy")]
            public Ycnenrruc Buy { get; set; }

            [JsonProperty("sell")]
            public Ycnenrruc Sell { get; set; }

            [JsonProperty("item")]
            public string Item { get; set; }

            [JsonProperty("now")]
            public DateTimeOffset Now { get; set; }
        }

        public sealed class Ycnenrruc {
            [JsonProperty("currency")]
            public string Currency { get; set; }

            [JsonProperty("display")]
            public string Display { get; set; }

            [JsonProperty("display_short")]
            public string DisplayShort { get; set; }

            [JsonProperty("value")]
            public double Value { get; set; }

            [JsonProperty("value_int")]
            public long ValueInt { get; set; }
        }
    }
}
