using CryptoExchange.Net.Objects;

using Ixcent.CryptoTerminal.Application.Exceptions;
using Ixcent.CryptoTerminal.Domain.Database.Models;
using Ixcent.CryptoTerminal.EFData;

namespace Ixcent.CryptoTerminal.Application
{
    /// <summary>
    /// Extensions for <see cref="WebCallResult{T}"/>
    /// </summary>
    public static class WebCallResultExtensions
    {
        /// <summary>
        /// Checks if exchange token is valid. 
        /// If not, removes this token from a database and throws a <see cref="RestException"/> instance
        /// </summary>
        /// <param name="context">Database</param>
        /// <param name="token">Token to validate</param>
        /// <exception cref="RestException"></exception>
        public static WebCallResult<T> RemoveTokenAndThrowRestIfInvalid<T>(this WebCallResult<T> source,
                                                                           CryptoTerminalContext context,
                                                                           ExchangeToken token)
        {
            int[] badTokenCodes = { -2014, -2015, -1022 };

            if (source.Success) return source;

            int errorCode = source.Error!.Code!.Value;

            if (badTokenCodes.Contains(errorCode))
            {
                context.ExchangeTokens.Remove(token);
                throw new RestException(System.Net.HttpStatusCode.BadRequest,
                                        ErrorCode.BadExchangeToken,
                                        new { Message = "Invalid API Token" });
            }

            return source;
        }
    }
}
