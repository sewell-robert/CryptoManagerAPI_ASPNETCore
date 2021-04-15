﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoManagerAPI.Models
{
    public class ExistingInvestment
    {
        [JsonProperty(PropertyName = "id")]
        public string ID { get; set; }

        [JsonProperty(PropertyName = "dbGrouping")]
        public string DbGrouping { get; set; }

        [JsonProperty(PropertyName = "investmentID")]
        public string InvestmentID { get; set; }

        [JsonProperty(PropertyName = "userID")]
        public string UserID { get; set; }

        [JsonProperty(PropertyName = "assetID")]
        public string AssetID { get; set; }

        [JsonProperty(PropertyName = "amountUSD")]
        public decimal AmountUSD { get; set; }

        [JsonProperty(PropertyName = "quantity")]
        public decimal Quantity { get; set; }

        [JsonProperty(PropertyName = "averagePrice")]
        public decimal AveragePrice { get; set; }

        [JsonProperty(PropertyName = "entryDt")]
        public DateTime EntryDt { get; set; }

        [JsonProperty(PropertyName = "modifyDt")]
        public DateTime ModifyDt { get; set; }

        [JsonProperty(PropertyName = "partitionKey")]
        public int PartitionKey { get; set; }
    }
}
