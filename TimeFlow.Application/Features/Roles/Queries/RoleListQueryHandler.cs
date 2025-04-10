using MediatR;
using Microsoft.EntityFrameworkCore;
using TimeFlow.Application.Features.Roles.DTOs;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Infrastructure.Contracts.Roles;

namespace TimeFlow.Application.Features.Roles.Queries
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
            IQueryable<Role> queryable = _roleRepository.Get(cancellationToken: cancellationToken);

            // Për aplikimin e paginimit
            var totalCount = await queryable.CountAsync(cancellationToken);
            var roles = await queryable
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync(cancellationToken);

            var readModel = roles.Select(x =>
                new RolesModel
                {
                    Id = x.Id,
                    RoleName = x.RoleName,
                    Description = x.Description,
                    CreatedOn = x.CreatedOn,
                    ModifiedOn = x.ModifiedOn,
                    Status = (int)x.Status,
                });

            return new GeneralResponse<IEnumerable<RolesModel>>
            {
                Success = true,
                Message = "Role list.",
                Result = readModel,
                TotalCount = totalCount,
                PageSize = query.PageSize,
                PageNumber = query.PageNumber,
                TotalPages = (int)Math.Ceiling((double)totalCount / query.PageSize)
            };
        }

    }
}