using RestSharp;
using System;

namespace Compori.Shipping.Gls.Common
{
    public class ResponseException : Exception
    {
        /// <summary>
        /// Liefert das Antwortobjekt zurück.
        /// </summary>
        /// <value>Das Antwortobjekt.</value>
        public RestResponse Response { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseException" /> class.
        /// </summary>
        /// <param name="response">Das Antwortobjekt.</param>
        /// <param name="message">Die Meldung.</param>
        public ResponseException(RestResponse response, string message) : base(message)
        {
            Response = response;
        }
    }
}
