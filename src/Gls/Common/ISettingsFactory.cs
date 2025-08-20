namespace Compori.Shipping.Gls.Common
{
    public interface ISettingsFactory<out TSettings> where TSettings : Settings
    {
        /// <summary>
        /// Erstellt die Einstellungen.
        /// </summary>
        /// <returns>TSettings.</returns>
        TSettings Create();
    }
}
