using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtGox.Data {
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
}
