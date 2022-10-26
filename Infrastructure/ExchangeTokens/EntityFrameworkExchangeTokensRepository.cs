using AutoMapper;

using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Interfaces;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Data;
using Ixcent.CryptoTerminal.Domain.ExchangeTokens.Models.Repository;
using Ixcent.CryptoTerminal.Storage;

namespace Ixcent.CryptoTerminal.Infrastructure.ExchangeTokens
{
    public class EntityFrameworkExchangeTokensRepository: IExchangeTokenRepository
    {
        private readonly CryptoTerminalContext _context;

        private readonly IMapper _mapper;
        
        public EntityFrameworkExchangeTokensRepository(CryptoTerminalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public Task<IEnumerable<ExchangeToken>> GetTokensByUserId(string userId, CancellationToken cancellationToken = default)
        {
            IQueryable<ExchangeTokenEntity> tokens = _context.ExchangeTokens.Where(t => t.UserId.Equals(userId));
            
            return Task.FromResult((IEnumerable<ExchangeToken>)tokens.Select(t => _mapper.Map<ExchangeToken>(t)));
        }

        public async Task AddToken(string userId, ExchangeToken token, CancellationToken cancellationToken = default)
        {
            ExchangeTokenEntity? existingToken = _context.ExchangeTokens.FirstOrDefault(
                t => token.Exchange == t.Exchange &&
                     userId == t.UserId);
            
            if (existingToken != null)
            {
                existingToken.Key = token.Key;
                existingToken.Secret = token.Secret;
                _context.ExchangeTokens.Update(existingToken);
            }
            else
            {
                _context.ExchangeTokens.Add(
                    new ExchangeTokenEntity
                    {
                        Exchange = token.Exchange,
                        Key = token.Key,
                        Secret = token.Secret,
                        UserId = userId
                    });
            }

            await _context.SaveChangesAsync(cancellationToken);
        }
        
        public async Task RemoveToken(string userId, string exchange, CancellationToken cancellationToken = default)
        {
            ExchangeTokenEntity? possibleToken =
                _context.ExchangeTokens.FirstOrDefault(t => exchange == t.Exchange
                                                            && userId == t.UserId);

            _context.ExchangeTokens.Remove(possibleToken);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}