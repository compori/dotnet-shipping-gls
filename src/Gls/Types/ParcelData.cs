using Newtonsoft.Json;

namespace Compori.Shipping.Gls.Types
{
    public class ParcelData
    {
        /// <summary>
        /// Liefert die Track ID zurück.
        /// </summary>
        /// <value>Die Track ID.</value>
        [JsonProperty(PropertyName = "TrackID", NullValueHandling = NullValueHandling.Ignore)]
        public string TrackId { get; set; }
        
        /// <summary>
        /// Liefert die Paketnummer zurück.
        /// </summary>
        /// <value>Die Paketnummer.</value>
        [JsonProperty(PropertyName = "ParcelNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string ParcelNumber { get; set; }
    }
}
