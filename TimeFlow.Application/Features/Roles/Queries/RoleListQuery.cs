using MediatR;
using TimeFlow.Application.Features.Roles.DTOs;
using TimeFlow.Application.Paged;
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.Roles.Queries
{
    public class RoleListQuery : IRequest<GeneralResponse<PagedResult<RolesModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? RoleName { get; set; } 
    }
}
