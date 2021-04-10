using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoManagerAPI.Models
{
    public class ExistingInvestment
    {
        [JsonProperty(PropertyName = "photoCount")]
        public int PhotoCount { get; set; }

        [JsonProperty(PropertyName = "photoCount")]
        public int test1 { get; set; }

        [JsonProperty(PropertyName = "photoCount")]
        public int test2 { get; set; }

        [JsonProperty(PropertyName = "photoCount")]
        public int test3 { get; set; }

        [JsonProperty(PropertyName = "photoCount")]
        public int test4 { get; set; }

        [JsonProperty(PropertyName = "photoCount")]
        public int test { get; set; }
    }
}
