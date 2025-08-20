using RestSharp;
using System.Linq;

namespace Compori.Shipping.Gls.Common
{
    public static class RestResponseExtension
    {

        /// <summary>
        /// Gets the header integer value for a header name.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="name">The header name.</param>
        /// <param name="defaultValue">The default value if name is not found.</param>
        /// <returns>System.Int32.</returns>
        public static int GetHeaderValue(this RestResponse response, string name, int defaultValue)
        {
            return !int.TryParse(response.GetHeaderValue(name, ""), out var result) ? defaultValue : result;
        }

        /// <summary>
        /// Gets the header string value for a header name.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="name">The header name.</param>
        /// <param name="defaultValue">The default value if name is not found.</param>
        /// <returns>System.String.</returns>
        public static string GetHeaderValue(this RestResponse response, string name, string defaultValue)
        {
            if (response?.Headers == null)
            {
                return defaultValue;
            }
            name = name.ToUpperInvariant();
            var value = response.Headers.FirstOrDefault(v => name.Equals(v.Name.ToUpperInvariant()));
            
            return value?.Value == null ? defaultValue : value.Value;
        }

        /// <summary>
        /// Gets the correlation identifier.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns>System.String.</returns>
        public static string GetCorrelationId(this RestResponse response)
        {
            return response.GetHeaderValue("Correlation-Id", null);
        }
        /// <summary>
        /// Gets the request identifier.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns>System.String.</returns>
        public static string GetRequestId(this RestResponse response)
        {
            return response.GetHeaderValue("X-Request-Id", null);
        }
    }
}
