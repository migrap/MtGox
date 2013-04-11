using Newtonsoft.Json;

namespace MtGox.Data {
    public class Idkey {
        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }
    }
}
