using Newtonsoft.Json;

namespace MtGox.Models.Money {
    public sealed class IdkeyResponse {
        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }
    }
}
