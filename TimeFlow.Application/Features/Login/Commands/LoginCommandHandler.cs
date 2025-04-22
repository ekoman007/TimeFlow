using MediatR;
using TimeFlow.Application.Features.Login.Dtos;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Repositories;
using TimeFlow.Domain.Security;
using TimeFlow.Infrastructure.Contracts;
using TimeFlow.SharedKernel;

namespace TimeFlow.Application.Features.Login.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, GeneralResponse<LoginResponse>>
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public LoginCommandHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator jwtTokenGenerator,
            IRefreshTokenRepository refreshTokenRepository)
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<GeneralResponse<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var user = await _userRepository.GetUserByEmailAsync(request.Email, cancellationToken);
            var roleName = user.Role.RoleName;

            if (user == null || !_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
            {
                return new GeneralResponse<LoginResponse>
                {
                    Success = false,
                    Message = "Login failed",
                    Result = null
                };
            }

            var accessToken = _jwtTokenGenerator.GenerateToken(user);
            var refreshTokenValue = _jwtTokenGenerator.GenerateRefreshToken();
            var refreshToken = Domain.Aggregates.UsersAggregates.RefreshToken.Create(refreshTokenValue, DateTime.UtcNow.AddMinutes(2), user.Id);

            // Revoko tokenat e vjetër
            await _refreshTokenRepository.RevokeAllTokensForUserAsync(user.Id, cancellationToken);

            // Ruaj tokenin e ri
            await _refreshTokenRepository.AddAsync(refreshToken, cancellationToken);

            return new GeneralResponse<LoginResponse>
            {
                Success = true,
                Message = "Login successful",
                Result = new LoginResponse
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken.Token,
                    RoleName = roleName,
                }
            };
        }

    }
}
