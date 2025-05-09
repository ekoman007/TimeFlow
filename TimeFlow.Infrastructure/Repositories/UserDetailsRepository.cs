using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Domain.Repositories;
using TimeFlow.Infrastructure.Database;

namespace TimeFlow.Infrastructure.Repositories
{
    public class UserDetailsRepository : GenericRepository<ApplicationUserDetails, int>, IUserDetailsRepository
    {
        private readonly TimeFlowDbContext _dbContext;

        public UserDetailsRepository(TimeFlowDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApplicationUserDetails> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.ApplicationUserDetails
                .FirstOrDefaultAsync(ud => ud.UserId == userId, cancellationToken);
        }

        public async Task<List<ApplicationUserDetails>> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _dbContext.ApplicationUserDetails
                .Where(ud => ud.FullName.Contains(name))
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistsByUserIdAsync(int userId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.ApplicationUserDetails.AnyAsync(ud => ud.UserId == userId, cancellationToken);
        }

        public async Task<ApplicationUserDetails> GetUserDetailsByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _dbContext.ApplicationUserDetails
                .FirstOrDefaultAsync(ud => ud.FullName == name, cancellationToken);
        }
    }
}