using Microsoft.EntityFrameworkCore;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Infrastructure.Contracts;
using TimeFlow.Infrastructure.Database;

namespace TimeFlow.Infrastructure.Repositories
{
    public class RoleRepository : GenericRepository<Role, int>, IRoleRepository
    {
        private readonly TimeFlowDbContext _dbContext;

        public RoleRepository(TimeFlowDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> GetRoleByNameAsync(string rolename, CancellationToken cancellationToken)
        {
            return await _dbContext.Roles.AnyAsync(u => u.RoleName == rolename, cancellationToken);
        }

    }
}
