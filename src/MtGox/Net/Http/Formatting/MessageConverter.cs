using MtGox.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace MtGox.Net.Http.Formatting {
    public class MessageConverter : JsonConverter {
        public override bool CanConvert(Type objectType) {
            return typeof(Message).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            var jobject = JObject.Load(reader);
            var message = new Message();
            serializer.Populate(jobject.CreateReader(), message);

            reader = jobject[message.Private].CreateReader();

            if (message.Private.Equals("depth", StringComparison.InvariantCultureIgnoreCase)) {
                message.Data = serializer.Populate<Depth>(reader);
            }
            else if (message.Private.Equals("trade", StringComparison.InvariantCultureIgnoreCase)) {
                message.Data = serializer.Populate<Trade>(reader);
            }

            return message;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            throw new NotImplementedException();
        }
    }       
}
