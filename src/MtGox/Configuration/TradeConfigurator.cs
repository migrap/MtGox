using MtGox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;

namespace MtGox.Configuration {
    internal class TradeConfigurator : ITradeConfigurator {
        private MtGoxClient _client;
        private string _item;

        public TradeConfigurator(MtGoxClient client) {
            _client = client;
        }
        public ITradeConfigurator Item(string value) {
            _item = value;
            return this;
        }

        public IObservable<Trade> Build() {
            var channel = "{0}.{1}".FormatWith("trade", _item);
            var observable = _client.Messages.Where(x => x.Name == channel)
                .Select(x => x.Data)
                .OfType<Trade>();

            _client.Subscribe(channel);

            return observable;
        }
    }
}
