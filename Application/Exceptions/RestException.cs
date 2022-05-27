using System.Net;

namespace Ixcent.CryptoTerminal.Application.Exceptions
{
    /// <summary> Exception for MediatR handlers to throw errors with status code </summary>
    /// <remarks> Inherited from <see cref="Exception"/> </remarks>
    public class RestException : Exception
    {
        public RestException(HttpStatusCode code, ErrorCode errorCode, object? errors = null)
        {
            StatusCode = code;
            ErrorCode = errorCode;
            Errors = errors;
        }

        /// <summary> Http page status code </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary> Additional error information </summary>
        public object? Errors { get; set; }

        /// <summary> Server error code </summary>
        public ErrorCode ErrorCode { get; set; }
    }
}
