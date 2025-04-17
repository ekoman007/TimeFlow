using MediatR; 
using TimeFlow.Application.Features.Services.DTOs;
using TimeFlow.Application.Paged;
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.Services.Queries
{
    public class ServiceListQuery : IRequest<GeneralResponse<PagedResult<ServiceModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}