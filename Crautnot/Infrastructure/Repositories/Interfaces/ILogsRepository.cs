using Infrastructure.Entities;

namespace Infrastructure.Repositories.Interfaces; 

public interface ILogsRepository {
    Task Log(Exception exception);
    Task Log(Logs log);
}