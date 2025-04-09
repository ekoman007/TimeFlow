using System; 
using TimeFlow.Domain.Aggregates.UsersAggregates;

namespace TimeFlow.Infrastructure.Contracts
{
    public interface ITestUserRepository : IRepository<TestUser, int>
    {
        Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken);
    }
}
