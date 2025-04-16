using MediatR; 
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.Category.Commands
{
    public class CreateCategoryCommand : IRequest<GeneralResponse<int>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public int IndustryId { get; set; }
    }
}