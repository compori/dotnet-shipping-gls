using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Compori.Shipping.Gls.Services
{
    public class ShipmentServiceTests : BaseTest
    {
        private ShipmentService CreateService(string file)
        {
            var settingsFactory = this.TestContext.CreateSettingsFactory<Settings>(file);
            var restClientFactory = new RestClientFactory();
            var client = new Client(settingsFactory, restClientFactory);
            return new ShipmentService(client);
        }

        protected override void Setup()
        {
            base.Setup();
        }
    }
}
