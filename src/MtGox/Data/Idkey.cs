using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtGox.Data {
    public class Idkey {
        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }
    }
}
