using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoManagerAPI.ViewModels
{
    public class ExistingInvestmentResponseVM
    {
        [JsonProperty(PropertyName = "id")]
        public string ID { get; set; }

        [JsonProperty(PropertyName = "assetName")]
        public string AssetName { get; set; }

        [JsonProperty(PropertyName = "assetSymbol")]
        public string AssetSym { get; set; }

        [JsonProperty(PropertyName = "amountUSD")]
        public string AmountUSD { get; set; }

        [JsonProperty(PropertyName = "averagePrice")]
        public string AveragePrice { get; set; }

        [JsonProperty(PropertyName = "quantity")]
        public string Quantity { get; set; }

        [JsonProperty(PropertyName = "entryDt")]
        public DateTime EntryDt { get; set; }

        [JsonProperty(PropertyName = "modifyDt")]
        public DateTime ModifyDt { get; set; }
    }
}
