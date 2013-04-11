using Newtonsoft.Json;

namespace MtGox.Data {
    public class Depth {
        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; }

        [JsonProperty(PropertyName = "item")]
        public string Item { get; set; }

        [JsonProperty(PropertyName = "now")]
        public string Now { get; set; }

        [JsonProperty(PropertyName = "price")]
        public double Price { get; set; }

        [JsonProperty(PropertyName = "type")]
        public Market Type { get; set; }

        [JsonProperty(PropertyName = "volume")]
        public double Volume { get; set; }
    }
}
