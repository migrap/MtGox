using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MtGox.Net.Http {
    internal class HmacDelegatingHandlerConfigurator : IHmacDelegatingHandlerConfigurator {
        private SecureString _secret;
        private Encoding _encoding = System.Text.Encoding.UTF8;
        private Func<object> _nonce = () => DateTime.Now.Ticks;
        private Func<SecureString, string, Encoding, string> _hash = (key,value,encoding) => {
            var hash = new HMACSHA512(Convert.FromBase64String(key.GetString()));
            var buffer = encoding.GetBytes(value);
            return Convert.ToBase64String(hash.ComputeHash(buffer));
        };

        public HmacDelegatingHandler Build() {
            return new HmacDelegatingHandler(_secret, _encoding, _nonce, _hash);
        }

        public void Secret(SecureString value) {
            _secret = value;
        }

        public void Encoding(Encoding value) {
            _encoding = value;
        }

        public void Nonce(Func<object> value) {
            _nonce = value;
        }

        public void Hash(Func<SecureString, string, Encoding, string> value) {
            _hash = value;
        }
    }
}
