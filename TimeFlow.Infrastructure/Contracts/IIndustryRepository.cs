 
using TimeFlow.Domain.Aggregates.UsersAggregates;

namespace TimeFlow.Infrastructure.Contracts
{
    public interface IIndustryRepository : IRepository<Industry, int>
    {
        Task<bool> GetIndustryByNameAsync(string industryName, CancellationToken cancellationToken);
    }
}
