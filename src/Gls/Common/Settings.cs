using Newtonsoft.Json;
using System;

namespace Compori.Shipping.Gls.Common
{
    public class Settings
    {
        /// <summary>
        /// Liefert oder setzt die Basis URL zum API-Endpunkt zurück.
        /// </summary>
        /// <value>Die URL zu GLS z.B.  https://10.10.10.10:8443/backend/rs/shipments.</value>
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        /// <summary>
        /// Liefert oder setzt den HTTP Agent für den API-Client.
        /// </summary>
        /// <value>Der HTTP Agent für den API-Client.</value>
        [JsonProperty(PropertyName = "clientAgent")]
        public string ClientAgent { get; set; }

        /// <summary>
        /// Liefert den Standard Timeout Wert für die Verbindung.
        /// </summary>
        /// <value>Der Standard Timeout Wert für die Verbindung.</value>
        public static TimeSpan DefaultTimeout => new TimeSpan(0, 5, 0);

        /// <summary>
        /// Liefert den Timeout Wert für die Verbindung.
        /// </summary>
        /// <value>Der Timeout Wert für die Verbindung.</value>
        [JsonProperty(PropertyName = "timeout")]
        public TimeSpan Timeout { get; set; } = DefaultTimeout;

        /// <summary>
        /// Liefert einen Wert, der angibt ob TLS 1.3 verwendet werden darf.
        /// </summary>
        /// <value><c>true</c> wenn TLS 1.3 verwendet werden darf; andernfalls, <c>false</c>.</value>
        [JsonProperty(PropertyName = "enableTls13")]
        public bool EnableTls13 { get; set; }

        /// <summary>
        /// Liefert einen Wert, der angibt ob TLS 1.3 verwendet werden soll.
        /// </summary>
        /// <value><c>true</c> wenn TLS 1.3 verwendet werden soll; andernfalls, <c>false</c>.</value>
        [JsonProperty(PropertyName = "forceTls13")]
        public bool ForceTls13 { get; set; }

        /// <summary>
        /// Liefert oder setzt einen Wert, der angibt, ob die Anfragen und Antworten in protokolliert werden sollen.
        /// </summary>
        /// <value><c>true</c> wenn Anfragen und Antworten protokolliert werden sollen; andernfalls, <c>false</c>.</value>
        [JsonProperty(PropertyName = "trace")]
        public bool Trace { get; set; }
    }
}
