using System.Threading;
using System.Threading.Tasks;
using TimeFlow.Domain.Aggregates.UsersAggregates;

namespace TimeFlow.Domain.Repositories
{
    public interface ICategoryRepository : IRepository<Category, int>
    {
        Task<bool> GetCategoryByNameAsync(string name, CancellationToken cancellationToken);
    }
} 