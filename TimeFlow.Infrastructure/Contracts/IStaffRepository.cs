 
using TimeFlow.Domain.Aggregates.UsersAggregates;

namespace TimeFlow.Infrastructure.Contracts
{
    public interface IStaffRepository : IRepository<Staff, int>
    {
        Task<bool> GetStaffByNameAsync(string name, CancellationToken cancellationToken);
    }
}
