using Microsoft.EntityFrameworkCore; 
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Infrastructure.Contracts;
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

        public async Task<bool> GetIndustryByNameAsync(string industryName, CancellationToken cancellationToken)
        {
            return await _dbContext.Industries.AnyAsync(u => u.Name == industryName, cancellationToken);
        } 

    }
}
