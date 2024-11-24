using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Repositories;

public class NewsRepository : INewsRepository {
    private readonly MainDbContext context;
    private readonly IMemoryCache _cache;

    public NewsRepository(MainDbContext context, IMemoryCache cache) {
        this.context = context;
        _cache = cache;
    }

    public async Task Add(NewsDto model) {
        var entity = new News {
            ExchangeTokens = model.ExchangeTokens,
            Topic = model.Topic,
            ListingDate = model.ListingDate.ToUniversalTime(),
            PublishDate = model.PublishDate.ToUniversalTime(),
            Category = model.Category,
        };

        await context.News.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task InitializeCacheAsync() {
        var newsList = await context.News.ToListAsync();
        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromDays(30));

        foreach (var news in newsList) { 
            _cache.Set(news.Topic, news.Topic, cacheEntryOptions);
        }
    }

    public async Task<bool> IsNewsExist(string topic) {
        return await context.News.AnyAsync(x => x.Topic == topic);
    }

    public async Task<List<News>> GetNewsWithoutCalculatedLeverage() {
        return await context.News
                            .Include(net => net.ExchangeTokens)
                            .ThenInclude(x => x.TokenData)
                            .AsNoTracking()
                            .Where(n => n.PublishDate.AddDays(2) < DateTime.Today &&
                                        n.ExchangeTokens.Any(net => !context.DealInformation
                                                                     .Any(di => di.ExchangeTokenId == net.Id)))
                            .ToListAsync();
    }
}