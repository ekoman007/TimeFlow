

using TimeFlow.Domain.Aggregates.UsersAggregates;

namespace TimeFlow.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);
        Task AddAsync(User user);
        Task<bool> UserExistsAsync(string email);
        Task<List<User>> GetAllAsync();
        Task AddUserAsync(User user);
    }
}
