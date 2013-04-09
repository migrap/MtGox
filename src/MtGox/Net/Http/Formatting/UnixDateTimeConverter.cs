using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtGox.Net.Http.Formatting {
    public class UnixDateTimeConverter : DateTimeConverterBase {
        private static readonly DateTimeOffset Epoch = new DateTimeOffset(new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

        public override bool CanConvert(Type objectType) {
            return typeof(DateTimeOffset).IsAssignableFrom(objectType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            long val;
            if (value is DateTimeOffset) {
                val = ((DateTimeOffset)value).ToUnixTime();
            }
            else {
                throw new Exception("Expected date object value.");
            }
            writer.WriteValue(val);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            var value = reader.Value;

            long ticks = Convert.ToInt64(reader.Value);

            switch (reader.TokenType) {
                case JsonToken.String: 
                    return Epoch.AddMilliseconds(ticks / 1000);
                case JsonToken.Integer: 
                    return Epoch.AddSeconds(ticks);
                default: 
                    throw new Exception("Invalid token type: Cannot convert to DateTime");
            }
        }
    }
}