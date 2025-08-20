using Newtonsoft.Json;

namespace Compori.Shipping.Gls.Types
{
    public class CreateShipmentResult
    {
        /// <summary>
        /// Liefert die erzeugten Sendungsdaten zurück.
        /// </summary>
        /// <value>The created shipment.</value>
        [JsonProperty(PropertyName = "CreatedShipment")]
        public CreatedShipment CreatedShipment { get; set; }
    }
}
