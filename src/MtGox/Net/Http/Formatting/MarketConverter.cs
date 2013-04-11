using MtGox.Data;
using Newtonsoft.Json;
using System;

namespace MtGox.Net.Http.Formatting {
    public class MarketConverter : JsonConverter {
        public override bool CanConvert(Type objectType) {
            return objectType == typeof(Market);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            return Enum.Parse(objectType, reader.Value.ToString(), true);            
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            throw new NotImplementedException();
        }
    }
}
