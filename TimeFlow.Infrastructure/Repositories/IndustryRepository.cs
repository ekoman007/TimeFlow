using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Domain.Repositories;
using TimeFlow.Infrastructure.Database;

namespace TimeFlow.Infrastructure.Repositories
{
    public class IndustryRepository : GenericRepository<Industry, int>, IIndustryRepository
    {
        private readonly TimeFlowDbContext _dbContext;

        public IndustryRepository(TimeFlowDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> GetIndustryByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await _dbContext.Industries.AnyAsync(u => u.Name == name, cancellationToken);
        }
    }
}
