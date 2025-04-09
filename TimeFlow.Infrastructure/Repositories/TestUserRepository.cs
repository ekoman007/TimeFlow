
using Microsoft.EntityFrameworkCore;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Infrastructure.Contracts;
using TimeFlow.Infrastructure.Database;

namespace TimeFlow.Infrastructure.Repositories
{
    public class TestUserRepository : GenericRepository<TestUser, int>, ITestUserRepository
    {
        private readonly TimeFlowDbContext _context;

        public TestUserRepository(TimeFlowDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        } 

        public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _context.Users.AnyAsync(u => u.Email == email, cancellationToken);
        }
        

    }
}