using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtGox.Models {
    public class Trade {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("date")]
        [JsonConverter(typeof(DateTimeOffsetConverter))]
        public DateTimeOffset Datetime { get; set; }

        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("tid")]
        public string Tid { get; set; }

        [JsonProperty("item")]
        public string Item { get; set; }

        [JsonProperty("price_currency")]
        public string Currency { get; set; }

        [JsonProperty("trade_type")]
        public string At { get; set; }

        [JsonProperty("primary")]
        [JsonConverter(typeof(PrimaryConverter))]
        public bool Primary { get; set; }

        [JsonProperty("properties")]
        public string Properties { get; set; }

        public override string ToString() {
            return (new { Type, Datetime, Amount, Price, Tid, Item, Currency, At, Primary, Properties }).ToString();
        }
    }

    public class PrimaryConverter : JsonConverter {
        public override bool CanConvert(Type objectType) {
            return typeof(bool) == objectType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            var read = (string)reader.Value;

            if(read.Equals("Y", StringComparison.InvariantCultureIgnoreCase)) {
                return true;
            } else if(read.Equals("N", StringComparison.InvariantCultureIgnoreCase)) {
                return false;
            }
            throw new ArgumentException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {

        }
    }

    class DateTimeOffsetConverter : JsonConverter {
        private static DateTimeOffset Epoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
        public DateTimeOffsetConverter() {
        }

        public override bool CanConvert(Type objectType) {
            return objectType == typeof(double);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            if(reader.TokenType == JsonToken.Integer) {
                var value = (long)reader.Value;
                return Epoch.AddSeconds(value);                
            }
            throw new JsonReaderException(string.Format("Unexcepted token {0}", reader.TokenType));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            throw new NotImplementedException();
        }
    }
}