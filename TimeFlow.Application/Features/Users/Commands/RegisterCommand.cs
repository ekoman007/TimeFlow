using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TimeFlow.Application.DTOs;
using TimeFlow.Domain.Aggregates.Enums;

namespace TimeFlow.Application.Features.Users.Commands
{
    public class RegisterCommand : IRequest<UserDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public UserRole Role { get; set; }
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, UserDto>
    {
        // Implementation will be added later
        public Task<UserDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
} 
