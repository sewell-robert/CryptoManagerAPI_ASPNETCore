using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoManagerAPI.Models
{
    public class CMUser
    {
        [JsonProperty(PropertyName = "id")]
        public string ID { get; set; }

        [JsonProperty(PropertyName = "dbGrouping")]
        public string DbGrouping { get; set; }

        [JsonProperty(PropertyName = "userID")]
        public string UserID { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "password")]
        public decimal Password { get; set; }

        [JsonProperty(PropertyName = "entryDt")]
        public DateTime EntryDt { get; set; }

        [JsonProperty(PropertyName = "modifyDt")]
        public DateTime ModifyDt { get; set; }

        [JsonProperty(PropertyName = "partitionKey")]
        public int PartitionKey { get; set; }
    }
}
