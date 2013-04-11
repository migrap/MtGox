using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtGox.Models.Money {
    public sealed class DepthResponse {
        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("data")]
        public Atad Data { get; set; }

        public sealed class Atad {
            [JsonProperty("bids")]
            public Market Bids { get; set; }

            [JsonProperty("asks")]
            public Market Asks { get; set; }

            [JsonProperty("filter_min_price")]
            public Filter Min { get; set; }

            [JsonProperty("filter_max_price")]
            public Filter Max { get; set; }
        }

        public sealed class Market : List<Level> {
        }

        public sealed class Level {
            [JsonProperty("price")]
            public double Price { get; set; }

            [JsonProperty("amount")]
            public double Amount { get; set; }

            [JsonProperty("stamp")]
            public string Stamp { get; set; }
        }

        public sealed class Filter {
            [JsonProperty("value")]
            public double Value { get; set; }

            [JsonProperty("currency")]
            public string Currency { get; set; }
        }
    }
}
