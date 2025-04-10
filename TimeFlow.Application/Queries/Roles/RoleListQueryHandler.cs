using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Aggregates.UsersAggregates.Roles;
using TimeFlow.Infrastructure.Contracts.Roles;

namespace TimeFlow.Application.Queries.Roles
{
    public class RoleListQueryHandler : IRequestHandler<RoleListQuery, GeneralResponse<IEnumerable<RolesModel>>>
    {
        private readonly IRoleRepository _roleRepository;

        public RoleListQueryHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<GeneralResponse<IEnumerable<RolesModel>>> Handle(RoleListQuery query, CancellationToken cancellationToken = default)
        {
            IEnumerable<Role> role = await _roleRepository.Get(cancellationToken: cancellationToken).ConfigureAwait(false);
            IEnumerable<RolesModel> readModel = [];

            if (role.Any())
            {
                readModel = role.Select(x =>
                new RolesModel
                {
                    Id = x.Id,
                    RoleName = x.RoleName,
                    Description = x.Description, 
                    CreatedOn = x.CreatedOn, 
                    ModifiedOn = x.ModifiedOn, 
                    Status = (int)x.Status, 
                });
            }

            return new GeneralResponse<IEnumerable<RolesModel>>
            {
                Success = true,
                Message = "Role list.",
                Result = readModel
            };
        }
    }
}