using Newtonsoft.Json;

namespace Compori.Shipping.Gls.Types
{
    public class Consignee
    {
        [JsonProperty(PropertyName = "ConsigneeID", NullValueHandling = NullValueHandling.Ignore)]
        public string ConsigneeId { get; set; }
        
        [JsonProperty(PropertyName = "CostCenter", NullValueHandling = NullValueHandling.Ignore)]
        public string CostCenter { get; set; }

        /// <summary>
        /// Gets or sets the category Only PRIVATE or BUSINESS.
        /// </summary>
        /// <value>The category.</value>
        [JsonProperty(PropertyName = "Category", NullValueHandling = NullValueHandling.Ignore)]
        public string Category { get; set; }

        [JsonProperty(PropertyName = "Address")]
        public Address Address { get; set; }

    }
}
