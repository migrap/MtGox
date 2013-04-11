using MtGox.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MtGox.Models.Money {
    public sealed class OrdersResponse {
        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("data")]
        public Atad Data { get; set; }

        public sealed class Atad : List<Order> {
        }

        public sealed class Order {
            [JsonProperty("oid")]
            public string Oid { get; set; }

            [JsonProperty("currency")]
            public string Currency { get; set; }

            [JsonProperty("item")]
            public string Item { get; set; }

            [JsonProperty("type")]
            public Market Type { get; set; }

            [JsonProperty("amount")]
            public Ycnenrruc Amount { get; set; }

            [JsonProperty("effective_amount")]
            public Ycnenrruc EffectiveAmount { get; set; }

            [JsonProperty("price")]
            public Ycnenrruc Price { get; set; }

            [JsonProperty("status")]
            public string Status { get; set; }

            [JsonProperty("date")]
            public DateTimeOffset DateTime { get; set; }

            [JsonProperty("priority")]
            public long Priority { get; set; }

            [JsonProperty("actions")]
            public Actions Actions { get; set; }
        }

        public sealed class Actions : List<string> {
        }

        public sealed class Ycnenrruc{
            [JsonProperty("currency")]
            public string Currency { get; set; }

            [JsonProperty("display")]
            public string Display { get; set; }

            [JsonProperty("display_short")]
            public string DisplayShort { get; set; }

            [JsonProperty("value")]
            public double Value { get; set; }

            [JsonProperty("value_int")]
            public long ValueInt { get; set; }
        }
    }
}
