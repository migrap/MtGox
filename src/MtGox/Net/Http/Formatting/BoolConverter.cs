using Newtonsoft.Json;
using System;

namespace MtGox.Net.Http.Formatting {
    public class BoolConverter : JsonConverter {
        public override bool CanConvert(Type objectType) {
            return typeof(bool).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            var value = reader.Value.ToString();

            switch (reader.TokenType) {
                case JsonToken.String:
                    return ("N".Equals(value, StringComparison.InvariantCultureIgnoreCase)) ? false : true;
                default:
                    throw new Exception("Invalid token type: Cannot convert to Boolean");
            }

        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            throw new NotImplementedException();
        }
    }
}
