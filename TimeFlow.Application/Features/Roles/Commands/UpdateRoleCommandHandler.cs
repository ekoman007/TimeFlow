using MediatR; 
using TimeFlow.Application.Commands.Roles.Command;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Repositories;
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.Roles.Commands
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, GeneralResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork; 
        private readonly IRoleRepository _roleRepository;

        public UpdateRoleCommandHandler(IUnitOfWork unitOfWork, IRoleRepository roleRepository)
        {
            _unitOfWork = unitOfWork; 
            _roleRepository = roleRepository;
        }

        public async Task<GeneralResponse<int>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var roleExists = await _roleRepository.GetByIdAsync(request.Id, cancellationToken : cancellationToken);

            roleExists.ChangeRoleName(request.RoleName);
            roleExists.ChangeDescription(request.Description);

            await _roleRepository.UpdateAsync(roleExists, cancellationToken).ConfigureAwait(false);
            await _unitOfWork.Save(cancellationToken).ConfigureAwait(false);

            return new GeneralResponse<int>
            {
                Success = true,
                Message = "Role updated successfully.",
                Result = roleExists.Id
            };
        }
    }
}
