using System.Threading;
using System.Threading.Tasks;
using TimeFlow.Domain.Aggregates.UsersAggregates;

namespace TimeFlow.Domain.Repositories
{
    public interface IAddressRepository : IRepository<Address, int>
    {
        Task<bool> GetAddressByStreetAsync(string street, CancellationToken cancellationToken);
    }
} 