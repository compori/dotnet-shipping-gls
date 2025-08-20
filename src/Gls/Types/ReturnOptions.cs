using Newtonsoft.Json;

namespace Compori.Shipping.Gls.Types
{
    public class ReturnOptions
    {
        /// <summary>
        /// Liefert zurück, ob die Rückgabedaten die Labeldaten enthalten soll.
        /// </summary>
        /// <value><c>true</c> wenn die Rückgabedaten die Labeldaten enthalten; andernfalls, <c>false</c>.</value>
        [JsonProperty(PropertyName = "ReturnPrintData")]
        public bool ReturnPrintData { get; set; }

        /// <summary>
        /// Liefert zurück, ob die Rückgabedaten die Routingdaten enthalten soll.
        /// </summary>
        /// <value><c>true</c> wenn die Rückgabedaten die Routingdaten enthalten; andernfalls, <c>false</c>.</value>
        [JsonProperty(PropertyName = "ReturnRoutingInfo")]
        public bool ReturnRoutingInfo { get; set; }
    }
}
