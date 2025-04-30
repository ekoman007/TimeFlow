using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Infrastructure.Contracts;
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

        public async Task<ApplicationUserDetails> ByUserId(int userId, CancellationToken cancellationToken)
        {
            return await _dbContext.ApplicationUserDetails.FirstOrDefaultAsync(u => u.UserId == userId, cancellationToken);
        }

        public async Task<bool> ExistByUserId(int userId, CancellationToken cancellationToken)
        {
            return await _dbContext.ApplicationUserDetails.AnyAsync(u => u.UserId == userId, cancellationToken);
        }

        public async Task<bool> GetUserDetailsByNameAsync(string fullname, CancellationToken cancellationToken)
        {
            return await _dbContext.ApplicationUserDetails.AnyAsync(u => u.FullName == fullname, cancellationToken);
        }
    }
}