using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtGox.Models {
    [JsonConverter(typeof(MessageConverter))]
    public class Message {
        [JsonProperty(PropertyName = "channel")]
        public string Channel { get; set; }
        [JsonProperty(PropertyName = "channel_name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "op")]
        public string Operation { get; set; }
        [JsonProperty(PropertyName = "origin")]
        public string Origin { get; set; }
        [JsonProperty(PropertyName = "private")]
        public string Private { get; set; }
        
        public object Data { get; set; }

        public override string ToString() {
            return (new { Channel = Channel, Name = Name, Operation = Operation, Origin = Origin, Private = Private }).ToString();
        }
    }

    public class MessageConverter : JsonConverter{

        public override bool CanConvert(Type objectType) {
            return typeof(MessageConverter) == objectType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            var message = new Message();
            var jobject = JObject.Load(reader);

            serializer.Populate(jobject.CreateReader(), message);

            if(message.Channel == "dbf1dee9-4f2e-4a08-8cb7-748919a71b21") {
                message.Data = jobject["trade"].ToObject(typeof(Trade));
            }

            return message;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            
        }

        private bool FieldExists(string fieldName, JObject jObject) {
            return jObject[fieldName] != null;
        }
    }
}
