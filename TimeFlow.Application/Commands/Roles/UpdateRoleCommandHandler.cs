using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Aggregates.UsersAggregates.Roles;
using TimeFlow.Domain.Repositories;
using TimeFlow.Infrastructure.Contracts.Roles;

namespace TimeFlow.Application.Commands.Roles
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, GeneralResponse<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;

        public UpdateRoleCommandHandler(IUnitOfWork unitOfWork, IRoleRepository roleRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _roleRepository = roleRepository;
        }

        public async Task<GeneralResponse<int>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var roleExists = await _roleRepository.GetById(request.Id); 

            roleExists.ChangeRoleName(request.RoleName);
            roleExists.ChangeDescription(request.Description);

            await _roleRepository.Update(roleExists, cancellationToken).ConfigureAwait(false);
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