using RestSharp;

namespace Compori.Shipping.Gls.Common
{
    public class FaultResponsesException : ResponseException
    {
        /// <summary>
        /// Liefert den Fehlercode zurück.
        /// </summary>
        /// <value>Der Fehlercode.</value>
        public string Error { get; }

        /// <summary>
        /// Liefert das Argument zurück, welches den Fehler verursacht hat, zurück.
        /// </summary>
        /// <value>Das Argument zurück, welches den Fehler verursacht hat.</value>
        public string Argument { get; }

        /// <summary>
        /// Liefert die Server-Ausführungszeit zurück.
        /// </summary>
        /// <value>Die Server-Ausführungszeit.</value>
        public string ServerExecutionTime { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FaultResponsesException" /> class.
        /// </summary>
        /// <param name="response">Das Antwortobjekt.</param>
        /// <param name="message">Die Meldung.</param>
        /// <param name="error">Der Fehlercode.</param>
        /// <param name="argument">Das Argument zurück, welches den Fehler verursacht hat.</param>
        /// <param name="serverExecutionTime">Die Server-Ausführungszeit.</param>
        public FaultResponsesException(RestResponse response, string message, 
            string error = null, 
            string argument = null,
            string serverExecutionTime = null) : base(response, message)
        {
            this.Error = error;
            this.Argument = argument;
            this.ServerExecutionTime = serverExecutionTime;
        }
    }
}
