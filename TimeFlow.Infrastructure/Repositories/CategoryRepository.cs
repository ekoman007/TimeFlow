using Microsoft.EntityFrameworkCore; 
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Infrastructure.Contracts;
using TimeFlow.Infrastructure.Database;

namespace TimeFlow.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category, int>, ICategoryRepository
    {
        private readonly TimeFlowDbContext _dbContext;

        public CategoryRepository(TimeFlowDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> GetCategoryByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await _dbContext.Categories.AnyAsync(u => u.Name == name, cancellationToken);
        } 

    }
}
