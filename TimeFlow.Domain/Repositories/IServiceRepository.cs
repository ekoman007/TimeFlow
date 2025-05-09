using System.Threading;
using System.Threading.Tasks;
using TimeFlow.Domain.Aggregates.UsersAggregates;

namespace TimeFlow.Domain.Repositories
{
    public interface IServiceRepository : IRepository<Service, int>
    {
        Task<bool> GetServiceByNameAsync(string name, CancellationToken cancellationToken);
    }
} 