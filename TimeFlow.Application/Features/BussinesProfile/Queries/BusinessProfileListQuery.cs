using MediatR; 
using TimeFlow.Application.Features.BussinesProfile.DTOs;
using TimeFlow.Application.Paged;
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.BussinesProfile.Queries
{
    public class BusinessProfileListQuery : IRequest<GeneralResponse<PagedResult<BussinesProfileModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
