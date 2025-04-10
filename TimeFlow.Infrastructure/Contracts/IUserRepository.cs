using System;
using TimeFlow.Domain.Aggregates.UsersAggregates;

namespace TimeFlow.Infrastructure.Contracts
{
    public interface IUserRepository : IRepository<ApplicationUser, int>
    {
        Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken);
        Task<ApplicationUser?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
    }
}
