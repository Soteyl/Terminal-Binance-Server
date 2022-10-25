﻿using Ixcent.CryptoTerminal.Domain.Common.Interfaces;

namespace Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Contracts
{
    /// <summary>
    /// Empty query object for getting available exchange tokens for user. <para/>
    /// </summary>
    public class GetExchangeTokensQuery : IRequestBase<GetExchangeTokensResponse>
    { }
}
