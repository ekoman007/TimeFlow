using MediatR; 
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Queries.Roles
{
    public class RoleListQuery : IRequest<GeneralResponse<IEnumerable<RolesModel>>>
    {
        //
    }
}
