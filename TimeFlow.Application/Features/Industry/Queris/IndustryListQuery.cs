using MediatR;
using TimeFlow.Application.Features.Industry.DTOs;
using TimeFlow.Application.Responses;
using TimeFlow.Infrastructure.Repositories;

namespace TimeFlow.Application.Features.Industry.Queris
{
    public class IndustryListQuery : IRequest<GeneralResponse<IEnumerable<IndustryModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}