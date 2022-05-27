namespace Ixcent.CryptoTerminal.Application.Exceptions
{
    public enum ErrorCode
    {
        Unknown = 0,

        /// <summary>
        /// User's exchange token key or secret stored in database is wrong
        /// </summary>
        BadExchangeToken = 1000,

        /// <summary>
        /// Trying to add an item that already exists
        /// </summary>
        AlreadyExist,

        /// <summary>
        /// Item is not found
        /// </summary>
        NotFound,

        /// <summary>
        /// Data sended from client is invalid
        /// </summary>
        InvalidData
    }
}
