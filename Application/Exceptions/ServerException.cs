using System.Net;

using Ixcent.CryptoTerminal.Domain.Common;

namespace Ixcent.CryptoTerminal.Application.Exceptions
{
    /// <summary> Exception for MediatR handlers to throw errors with status code </summary>
    /// <remarks> Inherited from <see cref="Exception"/> </remarks>
    public class ServerException : Exception
    {
        public static ServerException MissingApiToken => new(ServerResponseCode.MissingApiToken, "Missing API token");

        public ServerException(string responseCode, string message = "", string source = "")
        {
            ResponseCode = responseCode;
            Message = message;
            Source = source;
        }

        /// <summary> Additional error information </summary>
        public new string Message { get; }
        
        /// <summary> Source of the exception </summary>
        public string Source { get;  }

        /// <summary> Server error code </summary>
        public string ResponseCode { get;  }
    }
}
