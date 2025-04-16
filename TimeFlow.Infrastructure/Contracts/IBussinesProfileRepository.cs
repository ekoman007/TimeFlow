

using TimeFlow.Domain.Aggregates.UsersAggregates;

namespace TimeFlow.Infrastructure.Contracts
{
    public interface IBussinesProfileRepository : IRepository<BusinessProfile , int>
    {
        Task<bool> GetBussinesProfileByNameAsync(string name, CancellationToken cancellationToken);
    }
}
