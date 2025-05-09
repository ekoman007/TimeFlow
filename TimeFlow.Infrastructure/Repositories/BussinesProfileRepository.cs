using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Domain.Repositories;
using TimeFlow.Infrastructure.Database;

namespace TimeFlow.Infrastructure.Repositories
{
    public class BussinesProfileRepository : GenericRepository<BusinessProfile, int>, IBussinesProfileRepository
    {
        private readonly TimeFlowDbContext _dbContext;

        public BussinesProfileRepository(TimeFlowDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> GetBusinessProfileByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await _dbContext.BusinessProfiles.AnyAsync(u => u.BusinessName == name, cancellationToken);
        }

        public async Task<bool> GetBussinesProfileByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await GetBusinessProfileByNameAsync(name, cancellationToken);
        }
    }
}
