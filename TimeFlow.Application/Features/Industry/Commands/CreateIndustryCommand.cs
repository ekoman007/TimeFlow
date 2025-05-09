using MediatR; 
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.Industry.Commands
{
    public class CreateIndustryCommand : IRequest<GeneralResponse<int>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
    }
}
