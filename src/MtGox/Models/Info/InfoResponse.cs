﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MtGox.Models.Info {
    public class InfoResponse {
        [JsonProperty("result")]
        public string Result { get; set; }
        [JsonProperty("data")]
        public Data Data { get; set; }
    }

    public class Data {
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

    public class Rights : List<string> {
    }

    public class Wallets : Dictionary<string, Wallet> {
    }

    public class Wallet{
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
}