using Newtonsoft.Json;
using System.Collections.Generic;

namespace Compori.Shipping.Gls.Types
{
    public class CreatedShipment
    {
        /// <summary>
        /// Liefert die Referenz für diese Paketsendung zurück.
        /// </summary>
        /// <value>Die Referenz für diese Paketsendung.</value>
        [JsonProperty(PropertyName = "ShipmentReference", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> ShipmentReference { get; set; }

        /// <summary>
        /// Liefert die Paketinformationen zurück.
        /// </summary>
        /// <value>Die Paketinformationen.</value>
        [JsonProperty(PropertyName = "ParcelData", NullValueHandling = NullValueHandling.Ignore)]
        public List<ParcelData> ParcelData { get; set; }

        /// <summary>
        /// Liefert die Labeldaten zurück.
        /// </summary>
        /// <value>Die Labeldaten.</value>
        [JsonProperty(PropertyName = "PrintData", NullValueHandling = NullValueHandling.Ignore)]
        public List<PrintData> PrintData { get; set; }
    }
}
