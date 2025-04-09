using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TimeFlow.Application.DTOs;
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
        private readonly IMapper _mapper;  // AutoMapper
        private readonly ILogger<AddUserCommandHandler> _logger;

        public AddUserCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IMapper mapper, ILogger<AddUserCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _mapper = mapper; // AutoMapper injection
            _logger = logger;
        }

        public async Task<Result> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            //_logger.LogInformation("Handling AddUserCommand for Username: {Username}", request.Username);

            //// Hash fjalëkalimi
            //var hashedPassword = _passwordHasher.HashPassword(request.Password);

            //// Krijo DTO-në që do të përdoret për të krijuar një entitet User
            //var addUserDto = new AddUserDto
            //{
            //    Username = request.Username,
            //    Email = request.Email,
            //    PasswordHash = hashedPassword,
            //    RoleId = 2 // mund të merrni vlerën nga ndonjë vend tjetër
            //};

            //// Përdor AutoMapper për të konvertuar DTO në User
            //var user = _mapper.Map<User>(addUserDto);

            //// Përdor UnitOfWork për të marrë IUserRepository dhe për të shtuar përdoruesin
            //var userRepository = _unitOfWork.GetRepository<IUserRepository>();
            //await userRepository.AddUserAsync(user); // Ruaj përdoruesin në repository
            //await _unitOfWork.CompleteAsync();  // Siguron që të gjitha ndryshimet të ruhen në një transaksion

            //_logger.LogInformation("User {Username} added successfully", request.Username);

            //return Result.Success("User added successfully.");
            return null;
        }
    }

}
