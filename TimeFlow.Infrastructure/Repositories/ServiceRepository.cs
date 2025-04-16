

using Microsoft.EntityFrameworkCore;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Infrastructure.Contracts;
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

        public async Task<bool> GetserviceByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await _dbContext.Services.AnyAsync(u => u.Name == name, cancellationToken);
        }
    }
}
