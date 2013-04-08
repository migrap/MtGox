using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtGox.Net.Http.Formatting {
    internal static partial class Extensions {
        public static T Populate<T>(this JsonSerializer serializer, JsonReader reader) where T : class,new() {
            var target = new T();
            serializer.Populate(reader, target);
            return target;
        }

        private const string InvalidUnixEpochErrorMessage = "Unix epoc starts January 1st, 1970";
        private static readonly DateTimeOffset Epoch = new DateTimeOffset(new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
        /// <summary>
        ///   Convert a long into a DateTime
        /// </summary>
        public static DateTimeOffset FromUnixTime(this Int64 self) {
            return Epoch.AddSeconds(self);
        }

        /// <summary>
        ///   Convert a DateTime into a long
        /// </summary>
        public static Int64 ToUnixTime(this DateTimeOffset self) {
            if (self == DateTimeOffset.MinValue) {
                return 0;
            }

            var delta = self - Epoch;

            if (delta.TotalSeconds < 0) {
                throw new ArgumentOutOfRangeException(InvalidUnixEpochErrorMessage);
            }

            return (long)delta.TotalSeconds;
        }
    }
}
