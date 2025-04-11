 
using TimeFlow.Domain.Aggregates.UsersAggregates;

namespace TimeFlow.Infrastructure.Contracts
{
    public interface IUserDetailsRepository : IRepository<ApplicationUserDetails, int>
    {
        Task<bool> GetUserDetailsByNameAsync(string rolename, CancellationToken cancellationToken);
        Task<bool> ExistByUserId(int userId, CancellationToken cancellationToken);
    }
}
