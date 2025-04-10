using AutoMapper;
using MediatR;
using TimeFlow.Application.Commands.Roles.Command;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Domain.Aggregates.UsersAggregates.Roles;
using TimeFlow.Domain.Repositories;
using TimeFlow.Infrastructure.Contracts;
using TimeFlow.Infrastructure.Contracts.Roles;

namespace TimeFlow.Application.Features.Roles.Commands
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, GeneralResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;

        public CreateRoleCommandHandler(IUnitOfWork unitOfWork, IRoleRepository roleRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _roleRepository = roleRepository;
        }

        public async Task<GeneralResponse<int>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var roleExists = await _roleRepository.GetRoleByNameAsync(request.RoleName, cancellationToken);
            if (roleExists)
            {
                return new GeneralResponse<int>
                {
                    Success = false,
                    Message = "Role with this email already exists."
                };
            }

            Role role = Role.Create(request.RoleName, request.Description);

            await _roleRepository.Add(role, cancellationToken).ConfigureAwait(false);
            await _unitOfWork.Save(cancellationToken).ConfigureAwait(false);

            return new GeneralResponse<int>
            {
                Success = true,
                Message = "Role created successfully.",
                Result = role.Id
            };
        }
    }
}
