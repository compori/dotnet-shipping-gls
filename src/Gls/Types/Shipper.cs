using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compori.Shipping.Gls.Types
{
    public class Shipper
    {
        [JsonProperty(PropertyName = "ContactID", NullValueHandling = NullValueHandling.Ignore)]
        public string ContactId { get; set; }

        [JsonProperty(PropertyName = "Address", NullValueHandling = NullValueHandling.Ignore)]
        public Address Address { get; set; }
    }
}
