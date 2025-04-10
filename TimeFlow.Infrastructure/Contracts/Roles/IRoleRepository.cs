using TimeFlow.Domain.Aggregates.UsersAggregates.Roles;

namespace TimeFlow.Infrastructure.Contracts.Roles
{
    public interface IRoleRepository : IRepository<Role, int>
    { 
        Task<bool> GetRoleByNameAsync(string rolename, CancellationToken cancellationToken);
    }
}
