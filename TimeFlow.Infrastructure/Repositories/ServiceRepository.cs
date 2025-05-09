using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Domain.Repositories;
using TimeFlow.Infrastructure.Database;

namespace TimeFlow.Infrastructure.Repositories
{
    public class ServiceRepository : GenericRepository<Service, int>, IServiceRepository
    {
        private readonly TimeFlowDbContext _dbContext;

        public ServiceRepository(TimeFlowDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> GetServiceByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await _dbContext.Services.AnyAsync(u => u.Name == name, cancellationToken);
        }
    }
}
