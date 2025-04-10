using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.Application.Commands.Roles.Command;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Repositories;
using TimeFlow.Infrastructure.Contracts.Roles;

namespace TimeFlow.Application.Features.Roles.Commands
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, GeneralResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;

        public DeleteRoleCommandHandler(IUnitOfWork unitOfWork, IRoleRepository roleRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _roleRepository = roleRepository;
        }

        public async Task<GeneralResponse<int>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);
            ;
            var roleExists = await _roleRepository.GetById(request.Id, cancellationToken: cancellationToken);

            if (roleExists == null)
            {
                return new GeneralResponse<int>
                {
                    Success = false,
                    Message = "Ky rol nuk u gjet"
                };
            }

            if (roleExists.Status == 0)
            {
                roleExists.ChangeStatusToActive();
            }
            else
            {
                roleExists.ChangeStatusToDelete();
            }

            await _roleRepository.Update(roleExists, cancellationToken).ConfigureAwait(false);
            await _unitOfWork.Save(cancellationToken).ConfigureAwait(false);

            return new GeneralResponse<int>
            {
                Success = true,
                Message = "Role deleted successfully.",
                Result = roleExists.Id
            };
        }
    }
}