using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Domain.Repositories;
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

        public async Task<bool> GetAddressByStreetAsync(string street, CancellationToken cancellationToken)
        {
            return await _dbContext.Addresses.AnyAsync(u => u.Street == street, cancellationToken);
        }
    }
}
