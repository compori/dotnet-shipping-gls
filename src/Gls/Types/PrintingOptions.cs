using Newtonsoft.Json;

namespace Compori.Shipping.Gls.Types
{
    public class PrintingOptions
    {
        /// <summary>
        /// Liefert das Format der Labels zurück.
        /// </summary>
        /// <value>Das Format der Labels.</value>
        [JsonProperty(PropertyName = "ReturnLabels", NullValueHandling = NullValueHandling.Ignore)]
        public ReturnLabels ReturnLabels { get; set; }
    }
}
