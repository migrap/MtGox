using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace MtGox.Net.Http.Formatting {
    internal class MtGoxMediaTypeFormatter : JsonMediaTypeFormatter {
        public MtGoxMediaTypeFormatter() {
            SerializerSettings.Converters.Add(new MessageConverter());
            SerializerSettings.Converters.Add(new MarketConverter());
            SerializerSettings.Converters.Add(new UnixDateTimeConverter());
        }

        public override bool CanReadType(Type type) {
            return true;
        }

        public override bool CanWriteType(Type type) {
            return true;
        }

        public override Task<object> ReadFromStreamAsync(Type type, System.IO.Stream readStream, System.Net.Http.HttpContent content, IFormatterLogger formatterLogger) {
            return base.ReadFromStreamAsync(type, readStream, content, formatterLogger);
        }

        public override Task WriteToStreamAsync(Type type, object value, System.IO.Stream writeStream, System.Net.Http.HttpContent content, System.Net.TransportContext transportContext) {
            return base.WriteToStreamAsync(type, value, writeStream, content, transportContext);
        }
    }
}