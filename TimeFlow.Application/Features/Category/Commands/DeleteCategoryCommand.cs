using MediatR; 
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.Category.Commands
{
    public class DeleteCategoryCommand : IRequest<GeneralResponse<int>>
    {
        public int Id { get; set; } 
    }
}