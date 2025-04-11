using MediatR; 
using TimeFlow.Application.Features.UserDetails.DTOs;
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.UserDetails.Queries
{
    public class UserDetailsListQuery : IRequest<GeneralResponse<IEnumerable<UserDetailsModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
