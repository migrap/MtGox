
namespace MtGox.Configuration {
    internal class MtGoxClientConfigurator : IMtGoxClientConfigurator {
        private string _scheme = "https";
        private string _host = "data.mtgox.com";
        private int _port = 443;
        //private string _path = "api/2/money";
        private string _path = "api/2";
        private string _key;
        private string _secret;

        public void Scheme(string value) {
            _scheme = value;
        }

        public void Host(string value) {
            _host = value;
        }

        public void Port(int value) {
            _port = value;
        }

        public void Path(string value) {
            _path = value;
        }

        public void Key(string value) {
            _key = value;
        }

        public void Secret(string value) {
            _secret = value;
        }

        public MtGoxClient Build() {
            return new MtGoxClient(_scheme, _host, _port, _path, _key, _secret);
        }
    }
}
