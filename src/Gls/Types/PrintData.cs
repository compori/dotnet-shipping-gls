using Newtonsoft.Json;

namespace Compori.Shipping.Gls.Types
{
    public class PrintData
    {
        /// <summary>
        /// Liefert die Daten des Labels zurück.
        /// </summary>
        /// <value>Die Daten des Labels.</value>
        [JsonProperty(PropertyName = "Data", NullValueHandling = NullValueHandling.Ignore)]
        public string Data { get; set; }

        /// <summary>
        /// Liefert das Format des Labels zurück.
        /// </summary>
        /// <value>Das Format des Labels.</value>
        [JsonProperty(PropertyName = "LabelFormat", NullValueHandling = NullValueHandling.Ignore)]
        public string LabelFormat { get; set; }
    }
}
