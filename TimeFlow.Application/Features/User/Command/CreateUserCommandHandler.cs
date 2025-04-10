using AutoMapper;
using MediatR;
using TimeFlow.Application.Features.User.Command;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Domain.Repositories;
using TimeFlow.Domain.Security;
using TimeFlow.Infrastructure.Contracts;
using TimeFlow.Infrastructure.Contracts.Roles;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, GeneralResponse<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IMapper _mapper;  // AutoMapper
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    public CreateUserCommandHandler(IUnitOfWork unitOfWork, 
        IUserRepository userRepository, 
        IPasswordHasher passwordHasher, 
        IMapper mapper,
        IRoleRepository roleRepository)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    public async Task<GeneralResponse<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var checkRoleExist = await _roleRepository.GetById(request.RoleId, cancellationToken: cancellationToken);
        if (checkRoleExist == null)
        {
            return new GeneralResponse<int>
            {
                Success = false,
                Message = "Role with this is not exists."
            };
        }

        var userExists = await _userRepository.ExistsByEmailAsync(request.Email, cancellationToken);
        if (userExists)
        {
            return new GeneralResponse<int>
            {
                Success = false,
                Message = "User with this email already exists."
            };
        }

        // Hash the password before saving
        string hashedPassword = _passwordHasher.HashPassword(request.Password);

        // Create the User object with the hashed password
        ApplicationUser user = ApplicationUser.Create(request.Username, request.Email, hashedPassword, request.RoleId);

        await _userRepository.Add(user, cancellationToken).ConfigureAwait(false);
        await _unitOfWork.Save(cancellationToken).ConfigureAwait(false);

        return new GeneralResponse<int>
        {
            Success = true,
            Message = "User created successfully.",
            Result = user.Id
        };
    }
}
