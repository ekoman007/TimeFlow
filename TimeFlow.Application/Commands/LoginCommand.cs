using MediatR; 
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Commands
{
    public class LoginCommand : IRequest<GeneralResponse<string>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}