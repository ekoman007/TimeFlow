using MediatR;
using TimeFlow.SharedKernel;

namespace TimeFlow.Application.Commands
{
    public class AddUserCommand : IRequest<Result> 
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
} 
