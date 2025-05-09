using MediatR;
using Microsoft.Extensions.Configuration; 
using TimeFlow.Application.Responses; 
using TimeFlow.Domain.Repositories;
using TimeFlow.Domain.Security;

namespace TimeFlow.Application.Features.RefreshToken.Commands
{
    public class GenerateRefreshTokenCommandHandler : IRequestHandler<GenerateRefreshTokenCommand, GeneralResponse<string>>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IConfiguration _config;

        public GenerateRefreshTokenCommandHandler(
            IRefreshTokenRepository refreshTokenRepository,
            IJwtTokenGenerator jwtTokenGenerator,
            IConfiguration config)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _config = config;
        }

        public async Task<GeneralResponse<string>> Handle(GenerateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            // Krijo Refresh Token
            var tokenValue = Guid.NewGuid().ToString(); // Generate a GUID or a different token generation method
            var expiration = DateTime.UtcNow.AddDays(7); // Expiration date for refresh token (7 days)

            var refreshToken = Domain.Aggregates.UsersAggregates.RefreshToken.Create(tokenValue, expiration, request.UserId);

            // Save token in the database
            await _refreshTokenRepository.AddAsync(refreshToken, cancellationToken);
            return new GeneralResponse<string>
            {
                Success = true,
                Message = "Role created successfully.",
                Result = refreshToken.Token
            };

             
        }
    }
}

