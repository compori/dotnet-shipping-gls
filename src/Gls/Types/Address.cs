using Newtonsoft.Json;

namespace Compori.Shipping.Gls.Types
{
    public class Address
    {
        /// <summary>
        /// Liefert den Namen der Anschrift zurück. (Pflichtfeld)
        /// </summary>
        /// <value>Der Name der Anschrift.</value>
        [JsonProperty(PropertyName = "Name1")]
        public string Name1 { get; set; }

        /// <summary>
        /// Liefert zusätzliche Informationen zum Namen der Anschrift zurück.
        /// </summary>
        /// <value>Die zusätzlichen Informationen der Anschrift.</value>
        [JsonProperty(PropertyName = "Name2", NullValueHandling = NullValueHandling.Ignore)]
        public string Name2 { get; set; }

        /// <summary>
        /// Liefert zusätzliche Informationen zum Namen der Anschrift zurück.
        /// </summary>
        /// <value>Die zusätzlichen Informationen der Anschrift.</value>
        [JsonProperty(PropertyName = "Name3", NullValueHandling = NullValueHandling.Ignore)]
        public string Name3 { get; set; }

        /// <summary>
        /// Liefert das Land (DE, AT, BE, FR, etc.) der Anschrift zurück. (Pflichtfeld)
        /// </summary>
        /// <value>Das Land DE, AT, BE, FR, etc.) der Anschrift.</value>
        [JsonProperty(PropertyName = "CountryCode")]
        public string CountryCode { get; set; }

        /// <summary>
        /// Liefert die Region, Staat bzw. Provinz der Anschrift zurück.
        /// </summary>
        /// <value>Die Region, Staat bzw. Provinz der Anschrift.</value>
        [JsonProperty(PropertyName = "Province", NullValueHandling = NullValueHandling.Ignore)]
        public string Province { get; set; }

        /// <summary>
        /// Liefert die Postleitzahl des Ortes zurück. (Pflichtfeld)
        /// </summary>
        /// <value>Die Postleitzahl des Ortes.</value>
        [JsonProperty(PropertyName = "ZIPCode")]
        public string ZipCode { get; set; }

        /// <summary>
        /// Liefert den Ort zurück. (Pflichtfeld)
        /// </summary>
        /// <value>Der Ort.</value>
        [JsonProperty(PropertyName = "City")]
        public string City { get; set; }

        /// <summary>
        /// Liefert die Straße zurück. (Pflichtfeld)
        /// </summary>
        /// <value>Die Straße.</value>
        [JsonProperty(PropertyName = "Street")]
        public string Street { get; set; }

        /// <summary>
        /// Liefert die Hausnummer zurück, sofern diese nicht in der Straße enthalten ist.
        /// </summary>
        /// <value>Die Hausnummer.</value>
        [JsonProperty(PropertyName = "StreetNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string StreetNumber { get; set; }

        /// <summary>
        /// Liefert die E-Mail-Adresse zurück.
        /// </summary>
        /// <value>Die E-Mail-Adresse.</value>
        [JsonProperty(PropertyName = "eMail", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        /// <summary>
        /// Liefert die Kontaktperson zurück.
        /// </summary>
        /// <value>Die Kontaktperson.</value>
        [JsonProperty(PropertyName = "ContactPerson", NullValueHandling = NullValueHandling.Ignore)]
        public string ContactPerson { get; set; }

        /// <summary>
        /// Liefert die Festnetznummer der Kontaktperson zurück.
        /// </summary>
        /// <value>Die Festnetznummer der Kontaktperson.</value>
        [JsonProperty(PropertyName = "FixedLinePhonenumber", NullValueHandling = NullValueHandling.Ignore)]
        public string Phone { get; set; }

        /// <summary>
        /// Liefert die Mobilnummer der Kontaktperson zurück.
        /// </summary>
        /// <value>Die Mobilnummer der Kontaktperson.</value>
        [JsonProperty(PropertyName = "MobilePhoneNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string Mobile { get; set; }
    }
}
