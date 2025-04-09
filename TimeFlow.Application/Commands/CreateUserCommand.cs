using MediatR;
using TimeFlow.Application.Responses;
using TimeFlow.SharedKernel;

namespace TimeFlow.Application.Commands
{
    public class CreateUserCommand :IRequest<GeneralResponse<int>> 
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; } 
    }
} 
