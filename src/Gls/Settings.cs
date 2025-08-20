using Newtonsoft.Json;

namespace Compori.Shipping.Gls
{
    public class Settings : Common.Settings
    {
        /// <summary>
        /// Liefert den Benutzernamen zurück.
        /// </summary>
        /// <value>Der Benutzername.</value>
        [JsonProperty(PropertyName = "user")]
        public string User { get; set; }

        /// <summary>
        /// Liefert das Passwort zum Benutzer zurück.
        /// </summary>
        /// <value>Das Passwort zum Benutzer.</value>
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
    }
}
