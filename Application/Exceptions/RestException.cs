using System.Net;

namespace Ixcent.CryptoTerminal.Application.Exceptions
{
    /// <summary> Exception for MediatR handlers to throw errors with status code </summary>
    /// <remarks> Inherited from <see cref="Exception"/> </remarks>
    public class RestException : Exception
    {
        public RestException(HttpStatusCode code, object? errors = null)
        {
            Code = code;
            Errors = errors;
        }

        public HttpStatusCode Code { get; }

        public object? Errors { get; set; }
    }
}
