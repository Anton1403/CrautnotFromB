using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Enums;

namespace Infrastructure.Repositories.Interfaces;

public interface ITokenDataRepository {
    Task Add(ExchangeEnum exchange, string tokenName, List<TokenDataDto> data);
    Task<List<TokenData>> GetTokenData(long exchangeTokenId);

    Task<Token?> GetToken(string name);
    Task<Token> AddToken(string name);

    Task<IList<ExchangeTokens>> GetExchangeTokens();
    Task<ExchangeTokens?> GetExchangeTokens(ExchangeEnum exchange, Token token);
    Task<ExchangeTokens> AddExchangeTokens(ExchangeEnum exchange, Token token);

}