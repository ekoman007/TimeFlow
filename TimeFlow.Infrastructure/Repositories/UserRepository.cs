
using Microsoft.EntityFrameworkCore;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Infrastructure.Contracts;
using TimeFlow.Infrastructure.Database;

namespace TimeFlow.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<ApplicationUser, int>, IUserRepository
    {
        private readonly TimeFlowDbContext _dbContext;

        public UserRepository(TimeFlowDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        } 

        public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _dbContext.ApplicationUsers.AnyAsync(u => u.Email == email, cancellationToken);
        }

        public async Task<bool> ExistsByIDAsync(int Id, CancellationToken cancellationToken)
        {
            return await _dbContext.ApplicationUsers.AnyAsync(u => u.Id == Id, cancellationToken);
        }

        public async Task<ApplicationUser?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _dbContext.ApplicationUsers
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        } 
    }
}