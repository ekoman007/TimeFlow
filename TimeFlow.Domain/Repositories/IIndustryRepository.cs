using System.Threading;
using System.Threading.Tasks;
using TimeFlow.Domain.Aggregates.UsersAggregates;

namespace TimeFlow.Domain.Repositories
{
    public interface IIndustryRepository : IRepository<Industry, int>
    {
        Task<bool> GetIndustryByNameAsync(string name, CancellationToken cancellationToken);
    }
} 