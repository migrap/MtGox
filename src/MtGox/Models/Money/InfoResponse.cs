using Newtonsoft.Json;
using System.Collections.Generic;

namespace MtGox.Models.Money {
    public sealed class InfoResponse {
        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("data")]
        public Atad Data { get; set; }

        public sealed class Atad {
            [JsonProperty("Login")]
            public string Login { get; set; }

            [JsonProperty("Index")]
            public string Index { get; set; }

            [JsonProperty("Id")]
            public string Id { get; set; }

            [JsonProperty("Rights")]
            public Rights Rights { get; set; }

            [JsonProperty("Language")]
            public string Language { get; set; }

            [JsonProperty("Created")]
            public string Created { get; set; }

            [JsonProperty("Last_Login")]
            public string LastLogin { get; set; }

            [JsonProperty("Wallets")]
            public Wallets Wallets { get; set; }

            [JsonProperty("Monthly_Volume")]
            Field MonthlyVolume { get; set; }

            [JsonProperty("Trade_Fee")]
            double TradeFee { get; set; }
        }

        public sealed class Field {
            [JsonProperty("currency")]
            public string Currency { get; set; }

            [JsonProperty("display")]
            public string Display { get; set; }

            [JsonProperty("value")]
            public double Value { get; set; }
        }

        public sealed class Rights : List<string> {
        }

        public sealed class Wallet {
            [JsonProperty("Balance")]
            Field Balance { get; set; }

            [JsonProperty("Operations")]
            public long Operations { get; set; }

            [JsonProperty("Daily_Withdraw_Limit")]
            Field DailyWithdrawlLimit { get; set; }

            [JsonProperty("Monthly_Withdraw_Limit")]
            Field MonthlyWithdrawlLimit { get; set; }


            [JsonProperty("Max_Withdraw")]
            Field MaxWithdrawlLimit { get; set; }

            [JsonProperty("Open_Orders")]
            Field OpenOrders { get; set; }
        }

        public sealed class Wallets : Dictionary<string, Wallet> {
        }
    }   
}