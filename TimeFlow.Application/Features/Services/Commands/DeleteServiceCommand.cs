using MediatR;
 
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.Services.Commands
{
    public class DeleteServiceCommand : IRequest<GeneralResponse<int>>
    {
        public int Id { get; set; }
    }
}
