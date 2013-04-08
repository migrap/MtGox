using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtGox.Net.Http.Formatting {
    public class UnixDateTimeConverter : DateTimeConverterBase {
        public override bool CanConvert(Type objectType) {
            return typeof(DateTime).IsAssignableFrom(objectType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            long val;
            if (value is DateTime) {
                val = ((DateTime)value).ToUnixTime();
            }
            else {
                throw new Exception("Expected date object value.");
            }
            writer.WriteValue(val);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            if (reader.TokenType != JsonToken.Integer) {
                throw new Exception("Wrong Token Type");
            }

            long ticks = (long)reader.Value;
            return ticks.FromUnixTime();
        }
    }
}