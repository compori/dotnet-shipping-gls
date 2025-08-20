using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Compori.Shipping.Gls.Common
{
    public class Client<TSettings> where TSettings : Settings
    {
        /// <summary>
        /// Der Logger
        /// </summary>
        private static readonly NLog.Logger Log = NLog.LogManager.GetLogger(Logging.Facility);

        /// <summary>
        /// Liefert Factory Objekt für das Ermitteln der Einstellungen zurück.
        /// </summary>
        /// <value>Das Factory Objekt für die Einstellungen.</value>
        private ISettingsFactory<TSettings> SettingsFactory { get; }

        /// <summary>
        /// Die Einstellungen
        /// </summary>
        private TSettings _settings;

        /// <summary>
        /// Liefert die Einstellungen zurück.
        /// </summary>
        /// <value>Die Einstellungen.</value>
        public TSettings Settings => _settings ??= SettingsFactory.Create();

        /// <summary>
        /// Liefert Factory Objekt für den Restclient zurück.
        /// </summary>
        /// <value>Das Factory Objekt für den Restclient.</value>
        private IRestClientFactory<TSettings> RestClientFactory { get; }

        /// <summary>
        /// Der Restclient.
        /// </summary>
        private RestClient _restClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="Client{TSettings}"/> class.
        /// </summary>
        /// <param name="settingsFactory">Das Factory Objekt für die Einstellungen.</param>
        /// <param name="restClientFactory">Das Factory Objekt für den Restclient.</param>
        public Client(ISettingsFactory<TSettings> settingsFactory, IRestClientFactory<TSettings> restClientFactory)
        {
            SettingsFactory = settingsFactory;
            RestClientFactory = restClientFactory;
        }

        #region Response Processing

        /// <summary>
        /// Processes the response with .
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="response">The generic response.</param>
        /// <param name="startTime">The start time.</param>
        /// <returns>Response&lt;TResult&gt;.</returns>
        private Response<TResult> ProcessResponse<TResult>(RestResponse<TResult> response, DateTime startTime)
        {
            Guard.AssertArgumentIsNotNull(response, nameof(response));

            _ = this.ProcessResponse((RestResponse)response, startTime);

            return new Response<TResult>(
                response.StatusCode,
                response.GetCorrelationId(),
                response.IsSuccessful ? response.Data : default);
        }

        /// <summary>
        /// Processes the response.
        /// </summary>
        /// <param name="response">Die Restantwort.</param>
        /// <param name="startTime">The start time.</param>
        /// <returns>Response.</returns>
        private Response ProcessResponse(RestResponse response, DateTime startTime)
        {
            this.ProcessResponse(this.Settings, response, startTime);

            return new Response(response.StatusCode, response.GetCorrelationId());
        }

        /*
        /// <summary>
        /// Tries the deserialize error response.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="response">The response.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private static bool TryDeserializeErrorResponse(string content, out ErrorsResponse response)
        {

            try
            {
                response = JsonConvert.DeserializeObject<ErrorsResponse>(content);
                return true;
            }
            catch
            {
            }

            response = null;
            return false;
        }
        */

        /// <summary>
        /// Wird beim Verarbeiten der Rückmeldung ausgeführt.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="response">The response.</param>
        /// <param name="startTime">The start time.</param>
        protected virtual void OnProcessResponse(Settings settings, RestResponse response, DateTime startTime)
        {
        }

        /// <summary>
        /// Erstellt für die Restanfrage und -antwort einen Trace.
        /// </summary>
        /// <param name="settings">Die Einstellungen.</param>
        /// <param name="response">Die Restantwort.</param>
        /// <param name="startTime">The start time.</param>
        /// <exception cref="ResponseException"></exception>
        private void ProcessResponse(Settings settings, RestResponse response, DateTime startTime)
        {
            Guard.AssertArgumentIsNotNull(response, nameof(response));

            var trace = settings?.Trace ?? false;

            // trace request and response
            if (trace)
            {
                this.Trace(response, startTime);
            }

            // request was successful
            if (response.IsSuccessful)
            {
                this.OnProcessResponse(settings, response, startTime);

                return;
            }

            // if trace not running, now run.
            if (!trace)
            {
                this.Trace(response, startTime);
            }

            this.OnProcessResponse(settings, response, startTime);

            // 400
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new ResponseException(response, "Ungültiger Aufruf.");
            }

            // 401
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedResponseException(response);
            }

            // 403
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new ResponseException(response, "You are not allowed to talk to this endpoint. This can either be due to a wrong authentication or when you're trying to reach an endpoint that your account isn't allowed to access.");
            }


            throw new ResponseException(response, response.ErrorMessage ?? "Unbekannter Fehler.");
        }

        #region Tracing

        /// <summary>
        /// Führt einen Trace auf die Abfrage aus.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="startTime">Der Startzeitpunkt.</param>
        private void Trace(RestResponse response, DateTime startTime)
        {
            TraceRequest(response.Request, startTime);
            TraceResponse(response);
        }

        /// <summary>
        /// Traces the request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="startTime">Der Startzeitpunkt.</param>
        private void TraceRequest(RestRequest request, DateTime startTime)
        {
            try
            {
                var sb = new StringBuilder();
                sb.AppendLine("Trace Request");
                var duration = DateTime.UtcNow.Subtract(startTime);

                sb.AppendLine("------------------------------------------------------------------");
                sb.AppendLine($"Resource:      {request.Resource}");
                sb.AppendLine($"Method:        {request.Method}");
                sb.AppendLine($"Duration:      {duration:G}");
                sb.AppendLine("------------------------------------------------------------------");
                sb.AppendLine("Parameters:");
                foreach (var parameter in request.Parameters)
                {
                    var value = "";
                    if (!string.IsNullOrEmpty(parameter.Name))
                    {
                        value += $"Name={parameter.Name},";
                    }
                    if (parameter.Value != null)
                    {
                        value += $"Value={parameter.Value}";
                    }
                    if (ContentType.Json.Equals(parameter.ContentType))
                    {
                        value += $"({parameter.ContentType}) " + JsonConvert.SerializeObject(parameter.Value);
                    }
                    value = value.Trim();
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        continue;
                    }

                    sb.AppendLine($"{value}");

                }
                Log.Trace(sb.ToString());
            }
            catch
            {
                // catching all errors
            }
        }

        /// <summary>
        /// Traces the response.
        /// </summary>
        /// <param name="response">The response.</param>
        private void TraceResponse(RestResponse response)
        {
            try
            {
                var sb = new StringBuilder();

                sb.AppendLine("Trace Response");
                sb.AppendLine("------------------------------------------------------------------");
                sb.AppendLine($"StatusCode:         {response.StatusCode}");
                sb.AppendLine($"StatusDescription:  {response.StatusDescription ?? "N/A"}");
                sb.AppendLine($"ProtocolVersion:    {response.Version?.ToString() ?? "N/A"}");
                sb.AppendLine("------------------------------------------------------------------");
                sb.AppendLine("Headers");

                if (response.Headers != null)
                {
                    foreach (var header in response.Headers)
                    {
                        var value = !string.IsNullOrWhiteSpace(header.Name) ? header.Name : "";

                        if (!string.IsNullOrWhiteSpace(header.Value))
                        {
                            value += $": {header.Value}";
                        }

                        if (!string.IsNullOrWhiteSpace(header.ContentType))
                        {
                            value += $" (Content-Type {header.ContentType})";
                        }

                        value = value.Trim();
                        if (string.IsNullOrWhiteSpace(value))
                        {
                            continue;
                        }

                        sb.AppendLine($"{value}");
                    }
                }

                sb.AppendLine("------------------------------------------------------------------");
                sb.AppendLine($"ContentEncoding: {string.Join(", ", response.ContentEncoding)}");
                sb.AppendLine($"ContentLength:   {response.ContentLength?.ToString("N0") ?? "N/A"}");
                sb.AppendLine($"ContentType:     {response.ContentType ?? "N/A"}");
                sb.AppendLine($"Content:         {response.Content ?? "N/A"}");

                Log.Trace(sb.ToString());
            }
            catch
            {
                // catching all errors
            }
        }

        #endregion

        #endregion

        /// <summary>
        /// Erstellt den REST Client sofern nicht bereits vorhanden und konfiguriert ihn.
        /// </summary>
        /// <returns>RestClient.</returns>
        /// <exception cref="ResponseException">Die Client Einstellungen konnten nicht ermittelt werden.</exception>
        private RestClient Create()
        {
            if (this._restClient != null)
            {
                return this._restClient;
            }

            // Erstelle den Client mit Access Token
            this._restClient = RestClientFactory.Create(this.Settings);
            return this._restClient;
        }

        /// <summary>
        /// Sendet eine Anfrage über den REST Client.
        /// </summary>
        /// <typeparam name="TOutput">Der erwartete Rückgabetyp.</typeparam>
        /// <param name="request">Das Anfrageobjekt.</param>
        /// <param name="method">Die zu verwendene HTTP Methode.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task&lt;TOutput&gt; representing the asynchronous operation.</returns>
        public async Task<Response<TOutput>> Execute<TOutput>(RestRequest request, Method method, CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNull(request, nameof(request));

            var client = Create();
            var startTime = DateTime.UtcNow;
            var response = await client.ExecuteAsync<TOutput>(request, method, cancellationToken).ConfigureAwait(false);

            return this.ProcessResponse(response, startTime);
        }

        /// <summary>
        /// Sendet eine Anfrage über den REST Client.
        /// </summary>
        /// <param name="request">Das Anfrageobjekt.</param>
        /// <param name="method">Die zu verwendene HTTP Methode.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task<Response> Execute(RestRequest request, Method method, CancellationToken cancellationToken = default)
        {
            Guard.AssertArgumentIsNotNull(request, nameof(request));

            var client = Create();
            var startTime = DateTime.UtcNow;
            var response = await client.ExecuteAsync(request, method, cancellationToken).ConfigureAwait(false);

            return this.ProcessResponse(response, startTime);
        }

        /// <summary>
        /// Create a POST Request an returns a result.
        /// </summary>
        /// <typeparam name="TInput">The input typ.</typeparam>
        /// <typeparam name="TOutput">The expected result type.</typeparam>
        /// <param name="uri">The URI component added the client base URL.</param>
        /// <param name="data">The input data.</param>
        /// <param name="urlParameters">The URL parameters.</param>
        /// <param name="queryParameters">The query parameters.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task&lt;TOutput&gt; representing the asynchronous operation.</returns>
        public async Task<Response<TOutput>> Post<TInput, TOutput>(
            TInput data,
            string uri = null,
            Dictionary<string, string> urlParameters = null,
            Dictionary<string, string> queryParameters = null,
            CancellationToken cancellationToken = default) where TInput : class
        {
            Guard.AssertArgumentIsNotNull(data, nameof(data));

            var request = uri == null ? new RestRequest() : new RestRequest(uri);

            request = request
                .ApplyUrlParameter(urlParameters)
                .ApplyQueryParameter(queryParameters)
                .AddJsonBody(data);

            return await Execute<TOutput>(request, Method.Post, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Create a POST Request and returns a result.
        /// </summary>
        /// <typeparam name="TInput">The input typ.</typeparam>
        /// <param name="uri">The URI component added the client base URL.</param>
        /// <param name="data">The input data.</param>
        /// <param name="urlParameters">The URL parameters.</param>
        /// <param name="queryParameters">The query parameters.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Response.</returns>
        public async Task<Response> Post<TInput>(string uri,
            TInput data,
            Dictionary<string, string> urlParameters = null,
            Dictionary<string, string> queryParameters = null,
            CancellationToken cancellationToken = default) where TInput : class
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(uri, nameof(uri));
            Guard.AssertArgumentIsNotNull(data, nameof(data));

            var request = uri == null ? new RestRequest() : new RestRequest(uri);
            if (urlParameters != null && urlParameters.Count > 0)
            {
                foreach (var parameter in urlParameters)
                {
                    request.AddUrlSegment(parameter.Key, parameter.Value);
                }
            }

            if (queryParameters != null && queryParameters.Count > 0)
            {
                foreach (var parameter in queryParameters)
                {
                    request.AddQueryParameter(parameter.Key, parameter.Value);
                }
            }

            return await Execute(request.AddJsonBody(data), Method.Post, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Sendet eine POST Anfragen an eine URI.
        /// </summary>
        /// <typeparam name="TOutput">Der Typ des Ergebnisses.</typeparam>
        /// <param name="uri">Die Zieladresse.</param>
        /// <param name="urlParameters">The URL parameters.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns><c>true</c> if request was successful, <c>false</c> otherwise.</returns>
        public async Task<Response<TOutput>> Post<TOutput>(string uri, Dictionary<string, string> urlParameters = null, CancellationToken cancellationToken = default) where TOutput : class
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(uri, nameof(uri));

            var request = uri == null ? new RestRequest() : new RestRequest(uri);

            return await Execute<TOutput>(
                request.ApplyUrlParameter(urlParameters),
                Method.Post,
                cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Sendet eine PATCH Anfrage über den REST Client.
        /// </summary>
        /// <typeparam name="TInput">Der Typ des Eingangsparameters.</typeparam>
        /// <typeparam name="TOutput">Der erwartete Rückgabetyp.</typeparam>
        /// <param name="uri">Die Zieladresse.</param>
        /// <param name="data">Die zu sendenden Daten.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task&lt;TOutput&gt; representing the asynchronous operation.</returns>
        public async Task<Response<TOutput>> Patch<TInput, TOutput>(string uri, TInput data, CancellationToken cancellationToken = default) where TInput : class
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(uri, nameof(uri));
            Guard.AssertArgumentIsNotNull(data, nameof(data));

            return await Execute<TOutput>(new RestRequest(uri).AddJsonBody(data), Method.Patch, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Sendet eine PATCH Anfragen an eine URI.
        /// </summary>
        /// <typeparam name="TInput">Der Typ des Eingangsparameters.</typeparam>
        /// <param name="uri">Die Zieladresse.</param>
        /// <param name="data">Die zu sendenden Daten.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns><c>true</c> if request was successful, <c>false</c> otherwise.</returns>
        public async Task Patch<TInput>(string uri, TInput data, CancellationToken cancellationToken = default) where TInput : class
        {
            Guard.AssertArgumentIsNotNullOrWhiteSpace(uri, nameof(uri));
            Guard.AssertArgumentIsNotNull(data, nameof(data));

            await Execute(new RestRequest(uri).AddJsonBody(data), Method.Patch, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Sendet eine DELETE Anfragen an eine URI.
        /// </summary>
        /// <param name="uri">Die Zieladresse.</param>
        /// <param name="urlParameters">Zusätzliche URL Parameters.</param>
        /// <param name="queryParameters">Zusätzliche Query Parameter.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Response.</returns>
        public async Task<Response> Delete(string uri, Dictionary<string, string> urlParameters = null, Dictionary<string, string> queryParameters = null, CancellationToken cancellationToken = default)
        {
            var request = uri == null ? new RestRequest() : new RestRequest(uri);
            return await Execute(
                request
                    .ApplyUrlParameter(urlParameters)
                    .ApplyQueryParameter(queryParameters), 
                Method.Delete,
                cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Sendet eine GET Anfragen an eine URI.
        /// </summary>
        /// <typeparam name="TOutput">Der erwartete Rückgabetyp.</typeparam>
        /// <param name="uri">Die Zieladresse.</param>
        /// <param name="urlParameters">Zusätzliche URL Parameters.</param>
        /// <param name="queryParameters">Zusätzliche Query Parameter.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>A Task&lt;TOutput&gt; representing the asynchronous operation.</returns>
        public async Task<Response<TOutput>> Get<TOutput>(string uri = null, Dictionary<string, string> urlParameters = null, Dictionary<string, string> queryParameters = null, CancellationToken cancellationToken = default)
        {
            var request = uri == null ? new RestRequest() : new RestRequest(uri);
            return await Execute<TOutput>(
                request
                    .ApplyUrlParameter(urlParameters)
                    .ApplyQueryParameter(queryParameters), 
                Method.Get, 
                cancellationToken).ConfigureAwait(false);
        }
    }
}
