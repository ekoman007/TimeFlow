
using Microsoft.EntityFrameworkCore;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Infrastructure.Contracts;
using TimeFlow.Infrastructure.Database;

namespace TimeFlow.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User, int>, IUserRepository
    {
        private readonly TimeFlowDbContext _dbContext;

        public UserRepository(TimeFlowDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        } 

        public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _dbContext.Users.AnyAsync(u => u.Email == email, cancellationToken);
        }
        public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        } 
    }
}