using Newtonsoft.Json;

namespace Compori.Shipping.Gls.Types
{
    public class Service
    {
        /// <summary>
        /// Liefert den Namen des Service zurück.
        /// </summary>
        /// <value>Der Namen des Service.</value>
        [JsonProperty(PropertyName = "ServiceName")]
        public string ServiceName { get; set; }
    }
}
