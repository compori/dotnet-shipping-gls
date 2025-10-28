using Newtonsoft.Json;

namespace Compori.Shipping.Gls.Types
{
    public class ServiceContainer
    {
        /// <summary>
        /// Liefert den Service zurück.
        /// </summary>
        /// <value>Der Service.</value>
        [JsonProperty(PropertyName = "Service", NullValueHandling = NullValueHandling.Ignore)]
        public Service Service { get; set; }
    }
}
