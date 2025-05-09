using MediatR;
using TimeFlow.Application.Features.Login.Dtos;
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.Login.Commands
{
    public class LoginCommand : IRequest<GeneralResponse<LoginResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
