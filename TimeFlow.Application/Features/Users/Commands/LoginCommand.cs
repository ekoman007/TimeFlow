using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TimeFlow.Application.DTOs;

namespace TimeFlow.Application.Features.Users.Commands
{
    public class LoginCommand : IRequest<AuthResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
    {
        // Implementation will be added later
        public Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
} 
