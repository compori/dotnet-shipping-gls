using RestSharp;

namespace Compori.Shipping.Gls.Common
{
    public class UnauthorizedResponseException : ResponseException
    {
        public UnauthorizedResponseException(RestResponse response) : base(response, "Something has gone wrong when authorizing with our API. Please check e.g. if you're trying to use your sandbox api key with an operation that can only be used with a live API key.")
        {
        }
    }
}
