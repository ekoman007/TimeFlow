using MediatR; 
using TimeFlow.Application.Features.User.DTOs;
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.User.Query
{
    public class UserListQuery : IRequest<GeneralResponse<IEnumerable<UserModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}