using Infrastructure.Entities;
using Infrastructure.Repositories.Interfaces;

namespace Infrastructure.Repositories; 

public class DealInfoRepository : IDealInfoRepository {
    private readonly MainDbContext context;

    public DealInfoRepository(MainDbContext context) {
        this.context = context;
    }

    public async Task Add(List<DealInformation> deals) {
        await context.AddRangeAsync(deals);
        await context.SaveChangesAsync();
    }
}