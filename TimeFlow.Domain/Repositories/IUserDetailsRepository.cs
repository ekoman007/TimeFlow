using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TimeFlow.Domain.Aggregates.UsersAggregates;

namespace TimeFlow.Domain.Repositories
{
    public interface IUserDetailsRepository : IRepository<ApplicationUserDetails, int>
    {
        Task<ApplicationUserDetails> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);
        Task<List<ApplicationUserDetails>> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<bool> ExistsByUserIdAsync(int userId, CancellationToken cancellationToken = default);
        Task<ApplicationUserDetails> GetUserDetailsByNameAsync(string name, CancellationToken cancellationToken = default);
    }
} 