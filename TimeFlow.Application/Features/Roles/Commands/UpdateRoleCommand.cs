using MediatR;
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Commands.Roles.Command
{
    public class UpdateRoleCommand : IRequest<GeneralResponse<int>>
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
    }
}

