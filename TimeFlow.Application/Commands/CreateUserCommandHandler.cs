using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TimeFlow.Infrastructure.Contracts;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Domain.Repositories;
using TimeFlow.Domain.Security;
using TimeFlow.SharedKernel;

namespace TimeFlow.Application.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, GeneralResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;  // AutoMapper
        private readonly ILogger<AddUserCommandHandler> _logger;
        private readonly ITestUserRepository _itestUserRepository;

        public CreateUserCommandHandler(IUnitOfWork unitOfWork, ITestUserRepository itestUserRepository, IPasswordHasher passwordHasher, IMapper mapper, ILogger<AddUserCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _mapper = mapper; // AutoMapper injection
            _logger = logger;
            _itestUserRepository = itestUserRepository;
        }

        public async Task<GeneralResponse<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var userExists = await _itestUserRepository.ExistsByEmailAsync(request.Email, cancellationToken);
            if (userExists)
            {
                return new GeneralResponse<int>
                {
                    Success = false,
                    Message = "User with this email already exists." 
                };
            }

            TestUser user = TestUser.Create(request.Name, request.Email, request.Phone);

            await _itestUserRepository.Add(user, cancellationToken).ConfigureAwait(false);

            await _unitOfWork.Save(cancellationToken).ConfigureAwait(false);

            return new GeneralResponse<int>
            {
                Success = true,
                Message = "Product created successfully.",
                Result = user.Id
            };
        }
    }

}
