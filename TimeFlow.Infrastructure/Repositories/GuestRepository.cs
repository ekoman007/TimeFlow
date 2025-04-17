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
