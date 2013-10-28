using MtGox.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtGox.Json.Converters {
    internal class MessageConverter : JsonConverter {
        public override bool CanConvert(Type objectType) {
            return typeof(MessageConverter) == objectType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            var message = new Message();
            var jobject = JObject.Load(reader);

            serializer.Populate(jobject.CreateReader(), message);

            if(message.Channel == "dbf1dee9-4f2e-4a08-8cb7-748919a71b21") {
                message.Data = jobject["trade"].ToObject<Trade>();
            } else if(message.Channel == "d5f06780-30a8-4a48-a2f8-7ed181b4a13f") {
                message.Data = jobject["ticker"].ToObject<Ticker>();
            }

            return message;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            throw new NotImplementedException();
        }

        private bool FieldExists(string fieldName, JObject jObject) {
            return jObject[fieldName] != null;
        }
    }
}
