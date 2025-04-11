using TimeFlow.Domain.Aggregates.UsersAggregates;

namespace TimeFlow.Infrastructure.Contracts
{
    public interface IRoleRepository : IRepository<Role, int>
    {
        Task<bool> GetRoleByNameAsync(string rolename, CancellationToken cancellationToken);
    }
}
