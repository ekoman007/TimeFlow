using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Domain.Repositories;
using TimeFlow.Infrastructure.Database;

namespace TimeFlow.Infrastructure.Repositories
{
    public class GuestRepository : GenericRepository<Guest, int>, IGuestRepository
    {
        private readonly TimeFlowDbContext _dbContext;

        public GuestRepository(TimeFlowDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        } 
        public async Task<bool> GetGuestByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await _dbContext.Guest.AnyAsync(u => u.FullName == name, cancellationToken);
        }
    }
}
