

using Microsoft.EntityFrameworkCore;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Infrastructure.Contracts;
using TimeFlow.Infrastructure.Database;

namespace TimeFlow.Infrastructure.Repositories
{
    public class AddressRepository : GenericRepository<Address, int>, IAddressRepository
    {
        private readonly TimeFlowDbContext _dbContext;

        public AddressRepository(TimeFlowDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> GetAddressByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await _dbContext.Addresses.AnyAsync(u => u.Street == name, cancellationToken);
        } 


    }
}
