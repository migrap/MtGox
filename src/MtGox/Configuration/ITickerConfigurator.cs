using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtGox.Configuration {
    public interface ITickerConfigurator {
        ITickerConfigurator Item(string value);
        ITickerConfigurator Currency(string value);
    }
}
