using System.Threading;
using System.Threading.Tasks;
using TimeFlow.Domain.Aggregates.UsersAggregates;

namespace TimeFlow.Domain.Repositories
{
    public interface IBusinessProfileRepository : IRepository<BusinessProfile, int>
    {
        Task<bool> GetBusinessProfileByNameAsync(string name, CancellationToken cancellationToken);
    }
} 