using System.Threading;
using System.Threading.Tasks;
using TimeFlow.Domain.Aggregates.UsersAggregates;

namespace TimeFlow.Domain.Repositories
{
    public interface IRoleRepository : IRepository<Role, int>
    {
        Task<bool> GetRoleByNameAsync(string name, CancellationToken cancellationToken);
    }
} 