using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoManagerAPI.ViewModels
{
    public class AuthenticationResponseVM
    {
        [JsonProperty(PropertyName = "isUserAuthenticated")]
        public bool IsUserAuthenticated { get; set; }
    }
}
