using System;

namespace MtGox.Models {
    public sealed class Ticker {
        public Value High { get; set; }

        public Value Low { get; set; }

        public Value Average { get; set; }

        public Value Vwap { get; set; }

        public Value Volume { get; set; }

        public Value Last { get; set; }

        public Value Buy { get; set; }

        public Value Sell { get; set; }

        public Value Item { get; set; }

        public DateTimeOffset Now { get; set; }
    }
}