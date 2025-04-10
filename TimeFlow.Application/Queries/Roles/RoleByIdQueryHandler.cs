using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Aggregates.UsersAggregates.Roles;
using TimeFlow.Infrastructure.Contracts.Roles;

namespace TimeFlow.Application.Queries.Roles
{
    public class RoleByIdQueryHandler : IRequestHandler<RoleByIdQuery, GeneralResponse<RolesModel>>
    {
        private readonly IRoleRepository _roleRepository;

        public RoleByIdQueryHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<GeneralResponse<RolesModel>> Handle(RoleByIdQuery query, CancellationToken cancellationToken = default)
        {
            // Merrni rolin me ID nga repository
            var role = await _roleRepository.GetById(query.Id, cancellationToken: cancellationToken).ConfigureAwait(false);


            if (role == null)
            {
                return new GeneralResponse<RolesModel>
                {
                    Success = false,
                    Message = "Role not found"
                };
            }

            // Kthejeni në modelin e duhur
            var roleModel = new RolesModel
            {
                Id = role.Id,
                RoleName = role.RoleName,
                Description = role.Description,
                CreatedOn = role.CreatedOn,
                ModifiedOn = role.ModifiedOn,
                Status = (int)role.Status,
            };

            return new GeneralResponse<RolesModel>
            {
                Success = true,
                Message = "Role found successfully",
                Result = roleModel
            };
        }
    }
}
