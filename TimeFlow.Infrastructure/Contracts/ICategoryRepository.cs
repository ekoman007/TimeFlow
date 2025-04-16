 
using TimeFlow.Domain.Aggregates.UsersAggregates;

namespace TimeFlow.Infrastructure.Contracts
{
    public interface ICategoryRepository : IRepository<Category, int>
    {
        Task<bool> GetCategoryByNameAsync(string name, CancellationToken cancellationToken);
    }
}
