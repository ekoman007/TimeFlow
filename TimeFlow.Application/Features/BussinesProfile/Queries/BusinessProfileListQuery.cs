using MediatR; 
using TimeFlow.Application.Features.BussinesProfile.DTOs; 
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.BussinesProfile.Queries
{
    public class BusinessProfileListQuery : IRequest<GeneralResponse<IEnumerable<BussinesProfileModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}