using System.Net;

namespace Compori.Shipping.Gls.Common
{
    public class Response
    {
        /// <summary>
        /// Gets the http status code.
        /// </summary>
        /// <value>The status code.</value>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Gets the internal identifier for every request.
        /// </summary>
        /// <value>The internal identifier for every request.</value>
        public string CorrelationId { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Response{T}" /> class.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        /// <param name="correlationId">The request identifier.</param>
        public Response(HttpStatusCode statusCode, string correlationId)
        {
            StatusCode = statusCode;
            CorrelationId = correlationId;
        }
    }

    public class Response<T> : Response
    {
        /// <summary>
        /// Gets the result value.
        /// </summary>
        /// <value>The result value.</value>
        public T Result { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Response{T}" /> class.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="result">The result value.</param>
        public Response(HttpStatusCode statusCode, string requestId, T result) : base(statusCode, requestId)
        {
            Result = result;
        }
    }
}
