 
using TimeFlow.Domain.Aggregates.UsersAggregates;

namespace TimeFlow.Infrastructure.Contracts
{
    public interface IGuestRepository : IRepository<Guest, int>
    {
        Task<bool> GetGuestByNameAsync(string name, CancellationToken cancellationToken);
    }
}
