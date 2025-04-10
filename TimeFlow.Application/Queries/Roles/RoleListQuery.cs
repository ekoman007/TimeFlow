using MediatR; 
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Queries.Roles
{
    public class RoleListQuery : IRequest<GeneralResponse<IEnumerable<RolesModel>>>
    {
        public int PageNumber { get; set; } 
        public int PageSize { get; set; }  
    } 
}
