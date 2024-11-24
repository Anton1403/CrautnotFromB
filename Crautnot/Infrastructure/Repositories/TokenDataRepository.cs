using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Enums;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TokenDataRepository : ITokenDataRepository {
    private readonly MainDbContext context;

    public TokenDataRepository(MainDbContext context) {
        this.context = context;
    }

    public async Task Add(ExchangeEnum exchange, string tokenName, List<TokenDataDto> data) {

        var token = await GetToken(tokenName) ?? await AddToken(tokenName);

        var exchangeTokens = await GetExchangeTokens(exchange, token) ?? await AddExchangeTokens(exchange, token);

        foreach (var item in data) {
            if (await context.TokenData.FirstOrDefaultAsync(x => x.ExchangeTokensId == exchangeTokens.Id &&
                                                           x.Dtv == item.Dtv) != null) continue;

            var entity = new TokenData {
                ExchangeTokensId = exchangeTokens.Id,
                Dtv = item.Dtv.ToUniversalTime(),
                TradingVolume = item.TradingVolume,
                ClosingPrice = item.ClosingPrice,
                HighestPrice = item.HighestPrice,
                LowestPrice = item.LowestPrice,
                OpeningPrice = item.OpeningPrice,
            };

            await context.TokenData.AddAsync(entity);
        }

        await context.SaveChangesAsync();
    }

    public async Task<List<TokenData>> GetTokenData(long exchangeTokenId) {
        return await context.TokenData
                            .AsNoTracking()
                            .Include(x => x.ExchangeTokens.Token)
                            .Include(x => x.ExchangeTokens.Exchange)
                            .Where(x => x.ExchangeTokensId == exchangeTokenId)
                            .ToListAsync();
    }


    #region Token

    public async Task<Token?> GetToken(string name) {
        return await context.Token.FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<Token> AddToken(string name) {
        var entity = new Token {
            Name = name
        };

        await context.Token.AddAsync(entity);
        await context.SaveChangesAsync();

        return entity;
    }

    #endregion


    #region ExchangeToken

    public async Task<IList<ExchangeTokens>> GetExchangeTokens() {
        return await context.ExchangeTokens
                            .AsNoTracking()
                            .Include(x => x.Exchange)
                            .Include(x => x.Token)
                            .Include(x => x.News)
                            .ToListAsync();
    }

    public async Task<ExchangeTokens?> GetExchangeTokens(ExchangeEnum exchange, Token token) {
        return await context.ExchangeTokens.FirstOrDefaultAsync(
            x => x.ExchangeId == (int)exchange && x.TokenId == token.Id);
    }

    public async Task<ExchangeTokens> AddExchangeTokens(ExchangeEnum exchange, Token token) {
        var entity = new ExchangeTokens {
            ExchangeId = (int)exchange,
            TokenId = token.Id
        };

        await context.ExchangeTokens.AddAsync(entity);
        await context.SaveChangesAsync();

        return entity;
    }

    #endregion
}