using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace MtGox.Net.Http {
    internal interface IHmacDelegatingHandlerConfigurator {
        void Secret(SecureString value);
        void Encoding(Encoding value);
        void Nonce(Func<object> value);
        void Hash(Func<SecureString, string, Encoding, string> value);
    }

    internal static partial class Extensions {
        public static void Secret(this IHmacDelegatingHandlerConfigurator self, string value) {
            self.Secret(value.GetSecureString());
        }
    }
}
