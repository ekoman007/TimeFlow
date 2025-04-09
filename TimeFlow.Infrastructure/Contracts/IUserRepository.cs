using System; 
using TimeFlow.Domain.Aggregates.UsersAggregates;

namespace TimeFlow.Infrastructure.Contracts
{
    public interface IUserRepository : IRepository<User, int>
    {
        Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken);
        Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
    }
}
