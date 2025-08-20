using Compori.Shipping.Gls.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Compori.Shipping.Gls.Services
{
    public class ShipmentService
    {
        /// <summary>
        /// Gets the REST Client.
        /// </summary>
        /// <value>The REST Client.</value>
        private Client Client { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShipmentService"/> class.
        /// </summary>
        /// <param name="client">The REST Client.</param>
        public ShipmentService(Client client)
        {
            this.Client = client;
        }
        
        /// <summary>
        /// Erstellt einen neuen Versand.
        /// </summary>
        /// <param name="shipment">Der Versandauftrag.</param>
        /// <returns>CreateShipmentResult.</returns>
        public async Task<CreateShipmentResult> Create(CreateShipment shipment)
        {
            // Create Shipment
            var result = await this.Client.Post<CreateShipment, CreateShipmentResult>(shipment).ConfigureAwait(false);

            return result.Result;
        }
        
        /// <summary>
        /// Storniert einen Versandauftrag ab.
        /// 
        /// Dieser Vorgang versucht, ein zuvor erstelltes Paket zu stornieren.
        /// Der Erfolg dieses Vorgangs ist nicht garantiert.
        /// Wenn das Paket bereits bearbeitet wird, kann es nicht mehr storniert werden.
        /// Der Stornierungsvorgang selbst erfolgt in den meisten Fällen asynchron.
        /// Wenn das System zum Zeitpunkt der Anfrage nicht mit den zentralen Systemen von GLS verbunden ist,
        /// wird der Vorgang zu einem späteren Zeitpunkt wiederholt. Der REST-Dienst wartet nicht auf die Ausführung der Anfrage,
        /// sondern kehrt nach erfolgreicher Terminierung zurück.
        /// Rücksendungen können nur über Ihr zuständiges Depot storniert werden.
        /// Ein Webservice kann aufgerufen werden, hat jedoch keine operative Auswirkung.
        /// </summary>
        /// <param name="trackId">Die Sendungsnummer.</param>
        public async Task<CancelResult> Cancel(string trackId)
        {
            var result = await this.Client.Post<CancelResult>(
                $"cancel/{trackId}", 
                new Dictionary<string, string>
                {
                    { "trackId", trackId }
                }).ConfigureAwait(false);

            return result.Result;
        }

        /*
        /// <summary>
        /// Reads the specified shipment number.
        /// </summary>
        /// <param name="shipmentNumber">The shipment number.</param>
        /// <param name="includeDocuments">if set to <c>true</c> [include documents].</param>
        /// <param name="documentFormat">The document format.</param>
        /// <param name="printFormat">The print format.</param>
        /// <param name="retourePrintFormat">The retoure print format.</param>
        /// <param name="combine">if set to <c>true</c> [combine].</param>
        /// <returns>CreateShipmentsResult.</returns>
        public async Task<ShipmentsResult> GetAllowedServices(
            string shipmentNumber,
            bool includeDocuments = true,
            string documentFormat = "PDF",
            string printFormat = null,
            string retourePrintFormat = null,
            bool combine = true)
        {
            // Query parameters
            var parameters = new Dictionary<string, string>()
            {
                { "shipment" , shipmentNumber },
                { "includeDocs", includeDocuments ? "include" : "URL" },
                { "docFormat", documentFormat },
                { "combine", combine ? "true" : "false" },
            };
            if (!string.IsNullOrEmpty(printFormat))
            {
                parameters.Add("printFormat", printFormat);
            }
            if (!string.IsNullOrEmpty(retourePrintFormat))
            {
                parameters.Add("retourePrintFormat", retourePrintFormat);
            }
            var result = await this.Client.Get<ShipmentsResult>(
                "orders",
                null,
                parameters).ConfigureAwait(false);
         
            return result.Result;
        }
        */
    }
}
