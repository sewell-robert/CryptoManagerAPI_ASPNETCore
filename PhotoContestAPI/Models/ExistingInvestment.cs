using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoManagerAPI.Models
{
    public class ExistingInvestment
    {
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "userID")]
        public string UserID { get; set; }

        [JsonProperty(PropertyName = "assetID")]
        public string AssetID { get; set; }

        [JsonProperty(PropertyName = "amountUSD")]
        public decimal AmountUSD { get; set; }

        [JsonProperty(PropertyName = "quantity")]
        public decimal Quantity { get; set; }

        [JsonProperty(PropertyName = "entryDt")]
        public DateTime EntryDt { get; set; }

        [JsonProperty(PropertyName = "modifyDt")]
        public DateTime ModifyDt { get; set; }

        [JsonProperty(PropertyName = "partition")]
        public int Partition { get; set; }
    }
}
