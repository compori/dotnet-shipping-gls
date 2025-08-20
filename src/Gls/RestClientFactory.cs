using RestSharp.Authenticators;

namespace Compori.Shipping.Gls
{
    public class RestClientFactory : Common.RestClientFactory<Settings>
    {
        /// <summary>
        /// Erstellt einen Authenticator für den REST Client.
        /// </summary>
        /// <param name="settings">Die Einstellungen.</param>
        /// <returns>AuthenticatorBase.</returns>
        protected override AuthenticatorBase CreateAuthenticator(Settings settings)
        {
            return new HttpBasicAuthenticator(settings.User, settings.Password);
        }
    }
}
