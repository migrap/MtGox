using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtGox.Json.Converters {
    internal class PrimaryConverter : JsonConverter {
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
            throw new NotImplementedException();
        }
    }
}
