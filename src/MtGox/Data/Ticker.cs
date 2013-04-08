using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtGox.Data {
    public sealed class Ticker {
        [JsonProperty("avg")]
        public Average Average { get; set; }
    }

    public sealed class Average {
        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("display")]
        public string Display { get; set; }

        [JsonProperty("value")]
        public double Value { get; set; }
    }
}
