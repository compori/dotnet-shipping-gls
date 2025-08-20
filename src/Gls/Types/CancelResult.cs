using Newtonsoft.Json;

namespace Compori.Shipping.Gls.Types
{
    public class CancelResult
    {
        [JsonProperty(PropertyName = "TrackID")]
        public string TrackId { get; set; }

        [JsonProperty(PropertyName = "result")]
        public string Result { get; set; }
    }
}
