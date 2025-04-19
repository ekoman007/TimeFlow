using MediatR;
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

        public RefreshAccessTokenCommandHandler(
            IRefreshTokenRepository refreshTokenRepository,
            IUserRepository userRepository,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<GeneralResponse<string>> Handle(RefreshAccessTokenCommand request, CancellationToken cancellationToken)
        {
            // Merr token-in nga DB
            var refreshToken = await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken);

            if (refreshToken == null || refreshToken.IsRevoked || refreshToken.IsUsed || refreshToken.Expires < DateTime.UtcNow)
            {
                return new GeneralResponse<string>
                {
                    Success = false,
                    Message = "Invalid or expired refresh token."
                };
            }

            // Merr user-in
            var user = await _userRepository.GetById(refreshToken.UserId,cancellationToken:cancellationToken);
            if (user == null)
            {
                return new GeneralResponse<string>
                {
                    Success = false,
                    Message = "User not found."
                };
            }

            // Gjenero access token të ri
            var newAccessToken = _jwtTokenGenerator.GenerateToken(user);

            // Opcionale: Shëno token-in si të përdorur
            refreshToken.MarkAsUsed();
            await _refreshTokenRepository.UpdateAsync(refreshToken);

            return new GeneralResponse<string>
            {
                Success = true,
                Message = "Access token refreshed successfully.",
                Result = newAccessToken
            };
        }
    }
}