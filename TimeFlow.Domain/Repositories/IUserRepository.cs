using System.Threading;
using System.Threading.Tasks;
using TimeFlow.Domain.Aggregates.UsersAggregates;

namespace TimeFlow.Domain.Repositories
{
    public interface IUserRepository : IRepository<ApplicationUser, int>
    {
        Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken);
        Task<bool> ExistsByIDAsync(int id, CancellationToken cancellationToken);
        Task<ApplicationUser?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
    }
} 