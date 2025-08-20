using RestSharp;
using System.Collections.Generic;

namespace Compori.Shipping.Gls.Common
{
    public static class RestRequestExtension
    {
        /// <summary>
        /// Wendet die URL Parameter auf das Anfrage-Objekt des REST Aufrufs an.
        /// </summary>
        /// <param name="request">Das Anfrage Objekt.</param>
        /// <param name="parameters">Die Parameter.</param>
        /// <returns>RestRequest.</returns>
        public static RestRequest ApplyUrlParameter(this RestRequest request, Dictionary<string, string> parameters = null)
        {
            if(request == null)
            {
                return null;
            }

            if (parameters == null || parameters.Count == 0)
            {
                return request;
            }

            foreach (var parameter in parameters)
            {
                request.AddUrlSegment(parameter.Key, parameter.Value);
            }

            return request;
        }

        /// <summary>
        /// Wendet die Query Parameter auf das Anfrage-Objekt des REST Aufrufs an.
        /// </summary>
        /// <param name="request">Das Anfrage Objekt.</param>
        /// <param name="parameters">Die Parameter.</param>
        /// <returns>RestRequest.</returns>
        public static RestRequest ApplyQueryParameter(this RestRequest request, Dictionary<string, string> parameters = null)
        {
            if(request == null)
            {
                return null;
            }

            if (parameters == null || parameters.Count == 0)
            {
                return request;
            }

            foreach (var parameter in parameters)
            {
                request.AddQueryParameter(parameter.Key, parameter.Value);
            }

            return request;
        }
    }
}
