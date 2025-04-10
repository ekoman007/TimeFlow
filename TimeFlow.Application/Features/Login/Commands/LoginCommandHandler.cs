using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TimeFlow.Infrastructure.Contracts;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Domain.Repositories;
using TimeFlow.Domain.Security;
using TimeFlow.Infrastructure.Repositories;
using TimeFlow.Infrastructure.Security;
using TimeFlow.SharedKernel;

namespace TimeFlow.Application.Features.Login.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, GeneralResponse<string>>
    { 
        private readonly IPasswordHasher _passwordHasher; 
        private readonly IUserRepository _itestUserRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginCommandHandler(
            IUserRepository itestUserRepository,
            IPasswordHasher passwordHasher, 
            IJwtTokenGenerator jwtTokenGenerator)
        { 
            _passwordHasher = passwordHasher; 
            _itestUserRepository = itestUserRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<GeneralResponse<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var user = await _itestUserRepository.GetUserByEmailAsync(request.Email, cancellationToken);

            if (user == null || !_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
            {
                return new GeneralResponse<string>
                {
                    Success = false,
                    Message = "Login failed",
                    Result = null
                };
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new GeneralResponse<string>
            {
                Success = true,
                Message = "Login successful",
                Result = token
            };
        }

    }
}

