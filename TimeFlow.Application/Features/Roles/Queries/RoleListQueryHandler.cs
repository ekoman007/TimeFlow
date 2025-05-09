using MediatR;
using Microsoft.EntityFrameworkCore;
using TimeFlow.Application.Features.Roles.DTOs;
using TimeFlow.Application.Paged;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.Roles.Queries
{
    public class RoleListQueryHandler : IRequestHandler<RoleListQuery, GeneralResponse<PagedResult<RolesModel>>>
    {
        private readonly IRoleRepository _roleRepository;

        public RoleListQueryHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        } 
        public async Task<GeneralResponse<PagedResult<RolesModel>>> Handle(RoleListQuery query, CancellationToken cancellationToken = default)
        {
            var queryable = _roleRepository.GetQueryable(cancellationToken);

            var pagedResult = await queryable.ToPagedResultAsync(
                query.PageNumber,
                query.PageSize,
                x => new RolesModel
                {
                    Id = x.Id,
                    RoleName = x.RoleName,
                    Description = x.Description,
                    CreatedOn = x.CreatedOn,
                    ModifiedOn = x.ModifiedOn,
                    Status = (int)x.Status,
                },
                cancellationToken
            );

            return new GeneralResponse<PagedResult<RolesModel>>
            {
                Success = true,
                Message = "Role list.",
                Result = pagedResult
            };
        }

    }
}

