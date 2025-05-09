using System.Threading;
using System.Threading.Tasks;
using TimeFlow.Domain.Aggregates.UsersAggregates;

namespace TimeFlow.Domain.Repositories
{
    public interface IStaffRepository : IRepository<Staff, int>
    {
        Task<bool> GetStaffByNameAsync(string name, CancellationToken cancellationToken);
    }
} 