using Newtonsoft.Json;

namespace Compori.Shipping.Gls.Types
{
    public class ReturnLabels
    {
        /// <summary>
        /// Liefert das Template Set zurück.
        /// </summary>
        /// <value>Das Template Set.</value>
        [JsonProperty(PropertyName = "TemplateSet")]
        public string TemplateSet { get; set; } = "NONE";

        /// <summary>
        /// Liefert ads Label Format zurück.
        /// </summary>
        /// <value>Das Label Format.</value>
        [JsonProperty(PropertyName = "LabelFormat")]
        public string LabelFormat { get; set; } = "PDF";
    }
}
