using AutoMapper;
using MediatR;
using TimeFlow.Application.Commands;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Domain.Repositories;
using TimeFlow.Domain.Security;
using TimeFlow.Infrastructure.Contracts;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, GeneralResponse<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IMapper _mapper;  // AutoMapper
    private readonly IUserRepository _itestUserRepository;

    public CreateUserCommandHandler(IUnitOfWork unitOfWork, IUserRepository itestUserRepository, IPasswordHasher passwordHasher, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
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

        // Hash the password before saving
        string hashedPassword = _passwordHasher.HashPassword(request.Password);

        // Create the User object with the hashed password
        User user = User.Create(request.Username, request.Email, hashedPassword, request.RoleId);

        await _itestUserRepository.Add(user, cancellationToken).ConfigureAwait(false);
        await _unitOfWork.Save(cancellationToken).ConfigureAwait(false);

        return new GeneralResponse<int>
        {
            Success = true,
            Message = "User created successfully.",
            Result = user.Id
        };
    }
}
