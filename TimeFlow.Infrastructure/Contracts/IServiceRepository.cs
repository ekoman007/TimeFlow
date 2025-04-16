 
using TimeFlow.Domain.Aggregates.UsersAggregates;

namespace TimeFlow.Infrastructure.Contracts
{
    public interface IServiceRepository : IRepository<Service, int>
    {
        Task<bool> GetserviceByNameAsync(string name, CancellationToken cancellationToken);
    }
}
