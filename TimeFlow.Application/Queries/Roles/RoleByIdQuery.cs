using MediatR; 
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Queries.Roles
{
    public class RoleByIdQuery : IRequest<GeneralResponse<RolesModel>>
    {
        public int Id { get; set; } 
        public RoleByIdQuery(int id)
        {
            Id = id;
        }
    }
}
