using Newtonsoft.Json;
using System.Collections.Generic;

namespace Compori.Shipping.Gls.Types
{
    public class Shipment
    {
        /// <summary>
        /// Liefert die Referenz für diese Paketsendung zurück.
        /// </summary>
        /// <value>Die Referenz für diese Paketsendung.</value>
        [JsonProperty(PropertyName = "ShipmentReference", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> ShipmentReference { get; set; }

        /// <summary>
        /// Liefert das Versanddatum zurück. (Format YYYY-MM-DD)
        /// </summary>
        /// <value>Das Versanddatum.</value>
        [JsonProperty(PropertyName = "ShippingDate", NullValueHandling = NullValueHandling.Ignore)]
        public string ShippingDate { get; set; }

        /// <summary>
        /// Liefert die Incoterm Kennung für diese Sendung. Dargestellt durch 2 Ziffern
        /// </summary>
        /// <value>Die Incoterm Kennung.</value>
        [JsonProperty(PropertyName = "IncotermCode", NullValueHandling = NullValueHandling.Ignore)]
        public string IncotermCode { get; set; }

        /// <summary>
        /// Liefert Person, die mit der Paketerstellung begonnen hat, zurück.
        /// </summary>
        /// <value>Die Person, die mit der Paketerstellung begonnen hat.</value>
        [JsonProperty(PropertyName = "Identifier", NullValueHandling = NullValueHandling.Ignore)]
        public string Identifier { get; set; }

        /// <summary>
        /// Liefert die Middleware zurück, die für die Paketerstellung verwendet wurde, zurück. (Pflichtfeld)
        /// </summary>
        /// <value>Die Middleware zurück, die für die Paketerstellung verwendet wurde.</value>
        [JsonProperty(PropertyName = "Middleware")]
        public string Middleware { get; set; }

        /// <summary>
        /// Liefert die Produktkennung (PARCEL, EXPRESS) für diese Sendung zurück. (Pflichtfeld)
        /// </summary>
        /// <value>Die Produktkennung für diese Sendung.</value>
        [JsonProperty(PropertyName = "Product")]
        public string Product { get; set; }

        /// <summary>
        /// Ruft einen Wert ab oder legt einen Wert fest, der angibt, ob eine alternative Zustellung bei Express zulässig ist.
        /// </summary>
        /// <value><c>null</c> wenn kein Wert enthalten, <c>true</c> wenn alternative Zustellung erlaubt; andernfalls, <c>false</c>.</value>
        [JsonProperty(PropertyName = "ExpressAltDeliveryAllowed", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ExpressAltDeliveryAllowed { get; set; }

        /// <summary>
        /// Liefert den Empfänger zurück.
        /// </summary>
        /// <value>Der Empfänger.</value>
        [JsonProperty(PropertyName = "Consignee")]
        public Consignee Consignee { get; set; }
        
        /// <summary>
        /// Liefert den Versender zurück.
        /// </summary>
        /// <value>Der Versender.</value>
        [JsonProperty(PropertyName = "Shipper")]
        public Shipper Shipper { get; set; }

        /// <summary>
        /// Liefert die Versandpakete zurück.
        /// </summary>
        /// <value>Die Versandpakete.</value>
        [JsonProperty(PropertyName = "ShipmentUnit")]
        public List<ShipmentUnit> ShipmentUnits { get; set; }
    }
}
