using MediatR;
using Microsoft.Extensions.Logging;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Domain.Repositories;
using TimeFlow.Domain.Security;
using TimeFlow.SharedKernel;

namespace TimeFlow.Application.Commands
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ILogger<AddUserCommandHandler> _logger;

        public AddUserCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, ILogger<AddUserCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _logger = logger;
        }

        public async Task<Result> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Handling AddUserCommand for Username: {Username}", request.Username);

                // Hash fjalëkalimi
                var hashedPassword = _passwordHasher.HashPassword(request.Password);

                var user = new User
                {
                    Username = request.Username,
                    Email = request.Email,
                    PasswordHash = hashedPassword,
                    RoleId = 2
                };

                // Përdor UnitOfWork për të marrë IUserRepository
                var userRepository = _unitOfWork.GetRepository<IUserRepository>();

                await userRepository.AddUserAsync(user);
                await _unitOfWork.CompleteAsync();  // Siguron që të gjitha ndryshimet të ruhen në një transaksion

                _logger.LogInformation("User {Username} added successfully", request.Username);

                return Result.Success("User added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding user: {Username}", request.Username);
                throw;
            }
        }
    }
}
