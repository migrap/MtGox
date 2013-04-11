﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtGox.Models.Money {
    public sealed class IdkeyResponse {
        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }
    }
}
