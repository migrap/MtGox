using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtGox.Data {
    public sealed class Ticker {
        public double High { get; set; }

        public double Low { get; set; }

        public double Average { get; set; }

        public double Vwap { get; set; }

        public double Volume { get; set; }

        public double Last { get; set; }

        public double Buy { get; set; }

        public double Sell { get; set; }

        public string Item { get; set; }

        public DateTimeOffset Now { get; set; }
    }
}