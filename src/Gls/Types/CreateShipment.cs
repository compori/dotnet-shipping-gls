using Newtonsoft.Json;

namespace Compori.Shipping.Gls.Types
{
    public class CreateShipment
    {
        /// <summary>
        /// Liefert den Versandauftrag zurück. (Pflichtfeld)
        /// </summary>
        /// <value>Der Versandauftrag.</value>
        [JsonProperty(PropertyName = "Shipment")]
        public Shipment Shipment { get; set; }

        /// <summary>
        /// Liefert die Druckoptionen (z.B. für das Label) zurück. (Pflichtfeld)
        /// </summary>
        /// <value>Die Druckoptionen (z.B. für das Label).</value>
        [JsonProperty(PropertyName = "PrintingOptions")]
        public PrintingOptions PrintingOptions { get; set; }

        /// <summary>
        /// Liefert die Rückgabeoptionen zurück.
        /// </summary>
        /// <value>Die Rückgabeoptionen.</value>
        [JsonProperty(PropertyName = "ReturnOptions", NullValueHandling = NullValueHandling.Ignore)]
        public ReturnOptions ReturnOptions { get; set; }
    }
}
