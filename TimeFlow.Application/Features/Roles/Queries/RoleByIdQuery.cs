using MediatR;
using TimeFlow.Application.Features.Roles.DTOs;
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.Roles.Queries
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
