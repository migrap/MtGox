using MtGox.Json.Converters;
using Newtonsoft.Json;
using System;

namespace MtGox.Models {
    public sealed class Ticker {
        [JsonProperty("high")]
        public Value High { get; set; }

        [JsonProperty("low")]
        public Value Low { get; set; }

        [JsonProperty("average")]
        public Value Average { get; set; }

        [JsonProperty("vwap")]
        public Value Vwap { get; set; }

        [JsonProperty("volume")]
        public Value Volume { get; set; }

        [JsonProperty("last")]
        public Value Last { get; set; }

        [JsonProperty("buy")]
        public Value Buy { get; set; }

        [JsonProperty("sell")]
        public Value Sell { get; set; }

        [JsonProperty("item")]
        public string Item { get; set; }

        [JsonProperty("now")]
        [JsonConverter(typeof(DateTimeOffsetConverter))]
        public DateTimeOffset Datetime { get; set; }

        public override string ToString() {
            return (new { Datetime, Buy, Sell, Last, Volume, Average, High, Low, Vwap, Item }).ToString();
        }
    }
}