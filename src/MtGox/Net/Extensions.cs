using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace MtGox.Net {
    public static partial class Extensions {
        public static StreamContent GetStreamContent(this IEnumerable<byte> source) {
            var stream = new MemoryStream(source.ToArray());
            var content = new StreamContent(stream);
            content.Headers.Add("Content-Type", "application/json");

            return content;
        }

        public static Task<T> ReadAsAsync<T>(this HttpContent content, MediaTypeFormatter formatter) {
            return content.ReadAsAsync<T>(new[] { formatter });
        }
    }
}
