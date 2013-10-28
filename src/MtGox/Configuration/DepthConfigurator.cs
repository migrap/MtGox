using MtGox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;

namespace MtGox.Configuration {
    internal class DepthConfigurator : IDepthConfigurator {
        private MtGoxClient _client;
        private string _item;
        private string _currency;

        public DepthConfigurator(MtGoxClient client) {
            _client = client;
        }
        public IDepthConfigurator Item(string value) {
            _item = value;
            return this;
        }

        public IDepthConfigurator Currency(string value) {
            _currency = value;
            return this;
        }

        public IObservable<Depth> Build() {
            var channel = "{0}.{1}{2}".FormatWith("depth", _item, _currency);
            var observable = _client.Messages.Where(x => x.Name == channel)
                .Select(x => x.Data)
                .OfType<Depth>();

            _client.Subscribe(channel);

            return observable;
        }
    }
}
