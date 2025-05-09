using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.Application.Features.RefreshToken.Commands;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Repositories;
using TimeFlow.Domain.Security;
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.RefreshToken.Queries
{
    public class RefreshAccessTokenCommandHandler : IRequestHandler<RefreshAccessTokenCommand, GeneralResponse<string>>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly ILogger<RefreshAccessTokenCommandHandler> _logger;

        public RefreshAccessTokenCommandHandler(
            IRefreshTokenRepository refreshTokenRepository,
            IUserRepository userRepository,
            IJwtTokenGenerator jwtTokenGenerator,
            ILogger<RefreshAccessTokenCommandHandler> logger)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _logger = logger;
        }

        public async Task<GeneralResponse<string>> Handle(RefreshAccessTokenCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to refresh token for token value (first 5 chars): {RefreshTokenStart}", request.RefreshToken?.Length > 5 ? request.RefreshToken.Substring(0, 5) : request.RefreshToken);
            
            var refreshTokenEntity = await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken);
            _logger.LogInformation("Refresh token found in DB: {TokenFound}", refreshTokenEntity != null);

            if (refreshTokenEntity == null || refreshTokenEntity.IsRevoked || refreshTokenEntity.IsUsed || refreshTokenEntity.Expires < DateTime.UtcNow)
            {
                _logger.LogWarning("Refresh token validation failed. Details - IsNull: {IsNull}, IsRevoked: {IsRevoked}, IsUsed: {IsUsed}, Expires: {Expires}, IsExpired: {IsExpired}", 
                    refreshTokenEntity == null, 
                    refreshTokenEntity?.IsRevoked,
                    refreshTokenEntity?.IsUsed,
                    refreshTokenEntity?.Expires,
                    refreshTokenEntity?.Expires < DateTime.UtcNow);
                
                return new GeneralResponse<string>
                {
                    Success = false,
                    Message = "Invalid or expired refresh token."
                };
            }
            _logger.LogInformation("Refresh token validation passed for User ID: {UserId}", refreshTokenEntity.UserId);

            var user = await _userRepository.GetByIdAsync(refreshTokenEntity.UserId, cancellationToken);
             _logger.LogInformation("User found for refresh token: {UserFound}", user != null);
            if (user == null)
            {
                _logger.LogWarning("User with ID {UserId} not found for refresh token.", refreshTokenEntity.UserId);
                return new GeneralResponse<string>
                {
                    Success = false,
                    Message = "User not found."
                };
            }

            var newAccessToken = _jwtTokenGenerator.GenerateToken(user);
            _logger.LogInformation("New access token generated (first 10 chars): {AccessTokenStart}", newAccessToken?.Length > 10 ? newAccessToken.Substring(0, 10) : newAccessToken);

            try
            {
                refreshTokenEntity.MarkAsUsed();
                await _refreshTokenRepository.UpdateAsync(refreshTokenEntity);
                _logger.LogInformation("Successfully marked refresh token as used and updated in DB for User ID: {UserId}", refreshTokenEntity.UserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to mark refresh token as used or update DB for User ID: {UserId}", refreshTokenEntity.UserId);
                // Decide if you want to fail the whole operation or just log the error
                // Returning success here means the user gets a new access token, but the old refresh token might be reusable (security risk)
                // Consider returning Success = false if this fails:
                // return new GeneralResponse<string> { Success = false, Message = "Failed to update refresh token status." };
            }

            return new GeneralResponse<string>
            {
                Success = true,
                Message = "Access token refreshed successfully.",
                Result = newAccessToken
            };
        }
    }
}
