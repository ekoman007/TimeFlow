using System.Threading;
using System.Threading.Tasks;
using TimeFlow.Domain.Aggregates.UsersAggregates;

namespace TimeFlow.Domain.Repositories
{
    public interface IGuestRepository : IRepository<Guest, int>
    {
        Task<bool> GetGuestByNameAsync(string name, CancellationToken cancellationToken);
    }
} 