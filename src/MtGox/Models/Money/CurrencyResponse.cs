using Newtonsoft.Json;

namespace MtGox.Models.Money {
    public class CurrencyResponse {
        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("data")]
        public Atad Data { get; set; }

        public sealed class Atad {
            [JsonProperty("currency")]
            public string Currency { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("symbol")]
            public string Symbol { get; set; }

            [JsonProperty("decimals")]
            public int Decimals { get; set; }

            [JsonProperty("display_decimals")]
            public int DisplayDecimals { get; set; }

            [JsonProperty("symbol_position")]
            public string SymbolPosition { get; set; }

            [JsonProperty("virtual")]
            public bool Virtual { get; set; }

            [JsonProperty("ticker_channel")]
            public string TickerChannel { get; set; }

            [JsonProperty("depth_channel")]
            public string DepthChannel { get; set; }
        }
    }
}
