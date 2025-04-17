using MediatR; 
using TimeFlow.Application.Features.Staffs.DTOs;
using TimeFlow.Application.Paged;
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.Staffs.Queries
{
    public class StaffListQuery : IRequest<GeneralResponse<PagedResult<StaffModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}