using MediatR;
using TimeFlow.Application.Features.RefreshToken.DTOs;
using TimeFlow.Application.Features.Roles.DTOs;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Repositories;

namespace TimeFlow.Application.Features.RefreshToken.Queries
{
    public class GetRefreshTokenByValueQueryHandler : IRequestHandler<GetRefreshTokenByValueQuery, GeneralResponse<RefreshTokenModel>>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public GetRefreshTokenByValueQueryHandler(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<GeneralResponse<RefreshTokenModel>> Handle(GetRefreshTokenByValueQuery request, CancellationToken cancellationToken)
        {
            // Fetch refresh token from the repository
            var refreshToken = await _refreshTokenRepository.GetByTokenAsync(request.Token);
            if (refreshToken == null)
            {
                // Ktheni një përgjigje me mesazh të gabuar
                return new GeneralResponse<RefreshTokenModel>
                {
                    Message = "Token not found",
                    Success = false
                };
            }

            // Map entity to DTO (RefreshTokenModel)
            var refreshTokenModel = new RefreshTokenModel
            {
                Token = refreshToken.Token,
                Expires = refreshToken.Expires,
                UserId = refreshToken.UserId,
                IsUsed = refreshToken.IsUsed,
                IsRevoked = refreshToken.IsRevoked
            };

            // Ktheni përgjigjen me të dhënat
            return new GeneralResponse<RefreshTokenModel>
            {
                Success = true,
                Message = "Role found successfully",
                Result = refreshTokenModel
            }; 
        }
    }
}

