using AutoMapper;

using FluentValidation;

using Ixcent.CryptoTerminal.Domain.Common.Interfaces;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Interfaces;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Data;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Repository;
using Ixcent.CryptoTerminal.Storage;

using Microsoft.EntityFrameworkCore;

namespace Ixcent.CryptoTerminal.Infrastructure.ExchangeTokens
{
    public class EntityFrameworkExchangeTokensRepository : IExchangeTokenRepository
    {
        private readonly CryptoTerminalContext _context;

        private readonly IMapper _mapper;
        private readonly IValidatorResolver _validator;

        public EntityFrameworkExchangeTokensRepository(CryptoTerminalContext context, IMapper mapper, IValidatorResolver validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        public Task<GetTokensResult> Get(GetTokensRequest request, CancellationToken cancellationToken = default)
        {
            IQueryable<ExchangeTokenEntity>
                tokens = _context.ExchangeTokens.Where(t => t.UserId.Equals(request.UserId));
            return Task.FromResult(new GetTokensResult { Tokens = tokens.Select(t => _mapper.Map<ExchangeToken>(t)) });
        }

        public async Task<GetOneTokenResult> GetOne(GetOneTokenRequest request,
            CancellationToken cancellationToken = default)
        {
            return new GetOneTokenResult
            {
                Token = _mapper.Map<ExchangeToken>(await _context.ExchangeTokens.FirstOrDefaultAsync(t =>
                    t.UserId.Equals(request.UserId) && t.Exchange.Equals(request.Exchange), cancellationToken))
            };
        }

        public async Task Add(AddTokenRequest request, CancellationToken cancellationToken = default)
        {
            ExchangeTokenEntity? existingToken = _context.ExchangeTokens.FirstOrDefault(
                t => request.Exchange == t.Exchange &&
                     request.UserId == t.UserId);

            if (existingToken != null)
            {
                existingToken.Key = request.Token.Key;
                existingToken.Secret = request.Token.Secret;
                _context.ExchangeTokens.Update(existingToken);
            }
            else _context.ExchangeTokens.Add(_mapper.Map<ExchangeTokenEntity>(request));

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Remove(RemoveTokenRequest request, CancellationToken cancellationToken = default)
        {
            ExchangeTokenEntity? possibleToken = await _context.ExchangeTokens.FirstOrDefaultAsync(
                t => request.Exchange == t.Exchange && request.UserId == t.UserId, cancellationToken);

            _context.ExchangeTokens.Remove(possibleToken);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}