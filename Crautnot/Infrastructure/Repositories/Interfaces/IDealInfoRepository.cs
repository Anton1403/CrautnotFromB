using Infrastructure.Entities;

namespace Infrastructure.Repositories.Interfaces; 

public interface IDealInfoRepository {
    Task Add(List<DealInformation> deals);
}