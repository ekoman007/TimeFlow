using MediatR;
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.UserDetails.Commands
{
    public class DeleteUserDetailsCommand : IRequest<GeneralResponse<int>>
    {
        public int Id { get; set; } 
    }
}
