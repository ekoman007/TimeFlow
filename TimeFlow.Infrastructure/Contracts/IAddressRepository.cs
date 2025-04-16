 
using TimeFlow.Domain.Aggregates.UsersAggregates;

namespace TimeFlow.Infrastructure.Contracts
{
    public interface IAddressRepository : IRepository<Address, int>
    {
        Task<bool> GetAddressByNameAsync(string name, CancellationToken cancellationToken);
    }
}
