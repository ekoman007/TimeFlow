
using TimeFlow.Domain.Aggregates.UsersAggregates;

namespace TimeFlow.Domain.Repositories
{
    public interface IRefreshTokenRepository 
    {
        Task<RefreshToken> GetByTokenAsync(string token);
        Task<RefreshToken> GetByUserIdAsync(int userId);
        Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken);
        Task UpdateAsync(RefreshToken refreshToken);
        Task RemoveAsync(RefreshToken refreshToken);

        Task RevokeAllTokensForUserAsync(int userId, CancellationToken cancellationToken);
    }
}
