using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Domain.Repositories;
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

        public async Task<bool> GetRoleByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await _dbContext.Roles.AnyAsync(u => u.RoleName == name, cancellationToken);
        }
    }
}
