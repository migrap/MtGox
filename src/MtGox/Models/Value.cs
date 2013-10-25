using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtGox.Models {
    public struct Value {
        [JsonProperty("value")]
        public double Double { get; set; }
        [JsonProperty("value_int")]
        public int Integer { get; set; }
        [JsonProperty("display")]
        public string Display { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        public override bool Equals(object obj) {
            if(!(obj is Value)) {
                return false;
            }

            var other = (Value)obj;

            return Double == other.Double && Currency == other.Currency;
        }

        public override string ToString() {
            return (new { Double, Integer, Display, Currency }).ToString();
        }

        public static implicit operator double(Value value) {
            return value.Double;
        }

        public static implicit operator int(Value value) {
            return value.Integer;
        }

        public static implicit operator string(Value value) {
            return value.Display;
        }
    }
}
