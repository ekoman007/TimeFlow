
using Microsoft.EntityFrameworkCore;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Infrastructure.Contracts;
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

        public async Task<bool> GetBussinesProfileByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await _dbContext.BusinessProfiles.AnyAsync(u => u.BusinessName == name, cancellationToken);
        }
         

    }
}
