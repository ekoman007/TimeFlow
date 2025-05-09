using MediatR; 
using TimeFlow.Application.Features.Industry.DTOs; 
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.Industry.Queris
{
    public class IndustryByIdQuery : IRequest<GeneralResponse<IndustryModel>>
    {
        public int Id { get; set; }
        public IndustryByIdQuery(int id)
        {
            Id = id;
        }
    }
}

