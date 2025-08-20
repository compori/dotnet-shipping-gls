using System.Collections.Generic;
using Newtonsoft.Json;

namespace Compori.Shipping.Gls.Types
{
    public class ShipmentUnit
    {
        /// <summary>
        /// Liefert eine optionale Referenznummer für das Paket.
        /// </summary>
        /// <value>Eine optionale Referenznummer für das Paket.</value>
        [JsonProperty(PropertyName = "ShipmentUnitReference", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> ShipmentUnitReference { get; set; }

        /// <summary>
        /// Liefert das aktuelle Gewicht mit mindestens 200 g Unterschied zum Originalgewicht zurück.
        /// </summary>
        /// <value>Das aktuelle Gewicht.</value>
        [JsonProperty(PropertyName = "Weight")]
        public double Weight { get; set; }

        /// <summary>
        /// Liefert dem Paket hinzugefügte Notizen zurück.
        /// </summary>
        /// <value>Dem Paket hinzugefügte Notizen.</value>
        [JsonProperty(PropertyName = "Note1", NullValueHandling = NullValueHandling.Ignore)]
        public string Note1 { get; set; }

        /// <summary>
        /// Liefert dem Paket hinzugefügte Notizen zurück.
        /// </summary>
        /// <value>Dem Paket hinzugefügte Notizen.</value>
        [JsonProperty(PropertyName = "Note2", NullValueHandling = NullValueHandling.Ignore)]
        public string Note2 { get; set; }

        /// <summary>
        /// Liefert die Möglichkeit zur Übergabe einer eigenen Referenz für Netzwerkpartner zurück.
        /// </summary>
        /// <value>Die Möglichkeit zur Übergabe einer eigenen Referenz für Netzwerkpartner.</value>
        [JsonProperty(PropertyName = "PartnerParcelNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string PartnerParcelNumber { get; set; }
    }
}
