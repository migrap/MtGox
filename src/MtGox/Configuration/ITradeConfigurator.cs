using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtGox.Configuration {
    public interface ITradeConfigurator {
        ITradeConfigurator Item(string value);
    }
}
