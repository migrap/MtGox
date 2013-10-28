using MtGox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;

namespace MtGox.Configuration {
    internal class TickerConfigurator : ITickerConfigurator {
        private MtGoxClient _client;
        private string _item;
        private string _currency;
        public TickerConfigurator(MtGoxClient client) {
            _client = client;
        }

        public ITickerConfigurator Item(string value) {
            _item = value;
            return this;
        }

        public ITickerConfigurator Currency(string value) {
            _currency = value;
            return this;
        }

        public IObservable<Ticker> Build() {
            var channel = "{0}.{1}{2}".FormatWith("ticker", _item, _currency);
            var observable = _client.Messages.Where(x => x.Name == channel)
                .Select(x => x.Data)
                .OfType<Ticker>();

            _client.Subscribe(channel);

            return observable;
        }
    }
}
