using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtGox.Models {
    public sealed class Field {
        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("display")]
        public string Display { get; set; }

        [JsonProperty("value")]
        public double Value { get; set; }
    }
}
