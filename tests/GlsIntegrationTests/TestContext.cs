using Compori.Shipping.Gls.Common;

namespace Compori.Shipping.Gls
{
    public class TestContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestContext"/> class.
        /// </summary>
        public TestContext()
        {
        }

        /// <summary>
        /// Erstellt ein neues Factory-Objekt für die Einstellungen.
        /// </summary>
        /// <typeparam name="TSettings">Der Typ der Einstellungen.</typeparam>
        /// <param name="file">Der Dateiname.</param>
        /// <returns>ISettingsFactory&lt;TSettings&gt;.</returns>
        public ISettingsFactory<TSettings> CreateSettingsFactory<TSettings>(string file) where TSettings : Settings
        {
            var factory = new SettingsFactory<TSettings>();
            factory.ReadFromJsonFile(file);
            return factory;
        }
    }
}
