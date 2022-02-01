using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosDB
{
    public class Customer
    {

        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }
        [JsonProperty(PropertyName = "customername")]
        public string customername { get; set; }
        [JsonProperty(PropertyName = "customercity")]
        public string customercity { get; set; }
    }
}
