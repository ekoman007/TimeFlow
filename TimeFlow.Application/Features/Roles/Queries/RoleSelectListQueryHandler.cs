using MediatR;
using Microsoft.EntityFrameworkCore;
using TimeFlow.Application.Features.Roles.DTOs;
using TimeFlow.Application.Queries.Roles.TimeFlow.Application.Queries.Roles;
using TimeFlow.Infrastructure.Contracts.Roles;

namespace TimeFlow.Application.Features.Roles.Queries
{
    public class RoleSelectListQueryHandler : IRequestHandler<RoleSelectListQuery, List<RoleSelectListModel>>
    {
        private readonly IRoleRepository _roleRepository;

        public RoleSelectListQueryHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<List<RoleSelectListModel>> Handle(RoleSelectListQuery query, CancellationToken cancellationToken)
        {
            // Merrni rolet nga repository

            var roles = await _roleRepository
                .Get(cancellationToken: cancellationToken)
                .ToListAsync(cancellationToken);

            // Kthejeni vetëm Id dhe RoleName për çdo rol
            return roles.Select(role => new RoleSelectListModel
            {
                Id = role.Id,
                RoleName = role.RoleName
            }).ToList();
        }
    }
}
