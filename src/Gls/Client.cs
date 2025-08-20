using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Security;
using Compori.Shipping.Gls.Common;
using Newtonsoft.Json;
using RestSharp;

namespace Compori.Shipping.Gls
{
    public class Client : Client<Settings>
    {
        public Client(ISettingsFactory<Settings> settingsFactory, RestClientFactory restClientFactory) : base(settingsFactory, restClientFactory)
        {
        }

        protected override void OnProcessResponse(Common.Settings settings, RestResponse response, DateTime startTime)
        {
            base.OnProcessResponse(settings, response, startTime);

            if (response == null)
            {
                return;
            }

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var message = response.Headers
                    ?.FirstOrDefault(v => "message".Equals(v.Name, StringComparison.InvariantCultureIgnoreCase))?.Value;
                var error = response.Headers
                    ?.FirstOrDefault(v => "error".Equals(v.Name, StringComparison.InvariantCultureIgnoreCase))?.Value;
                var arguments = response.Headers
                    ?.FirstOrDefault(v => "args".Equals(v.Name, StringComparison.InvariantCultureIgnoreCase))?.Value;
                var serverExecutionTime = response.Headers
                    ?.FirstOrDefault(v =>
                        "serverExecutionTime".Equals(v.Name, StringComparison.InvariantCultureIgnoreCase))?.Value;

                throw new FaultResponsesException(response, message, error, arguments, serverExecutionTime);
            }
        }
    }
}
