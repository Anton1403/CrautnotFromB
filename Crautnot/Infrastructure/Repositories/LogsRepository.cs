using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Entities;
using Infrastructure.Repositories.Interfaces;

namespace Infrastructure.Repositories
{
    public class LogsRepository : ILogsRepository {
        private readonly MainDbContext context;
        public LogsRepository(MainDbContext context) {
            this.context = context;
        }

        public async Task Log(Exception exception) {
            await context.Logs.AddAsync(new Logs{
                Date = DateTime.UtcNow,
                Message = exception.Message
            });
            await context.SaveChangesAsync();
        }

        public async Task Log(Logs log) {
            await context.Logs.AddAsync(log);
            await context.SaveChangesAsync();
        }
    }
}
