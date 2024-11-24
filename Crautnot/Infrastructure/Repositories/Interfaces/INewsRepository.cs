using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Enums;

namespace Infrastructure.Repositories.Interfaces;

public interface INewsRepository {
    Task Add(NewsDto model);
    Task<bool> IsNewsExist(string topic);
    Task<List<News>> GetNewsWithoutCalculatedLeverage();
    Task InitializeCacheAsync();
}