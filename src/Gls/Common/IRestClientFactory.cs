using RestSharp;

namespace Compori.Shipping.Gls.Common
{
    public interface IRestClientFactory<in TSettings> where TSettings : Settings
    {
        /// <summary>
        /// Erstellt einen RestClient mit den angegebenen Einstellungen.
        /// </summary>
        /// <param name="settings">Die Einstellungen.</param>
        /// <returns>RestClient.</returns>
        RestClient Create(TSettings settings);
    }
}
