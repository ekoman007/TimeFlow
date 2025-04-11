using MediatR; 
using TimeFlow.Application.Features.User.DTOs;
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.User.Query
{
    public class UserListQuery : IRequest<GeneralResponse<IEnumerable<ApplicationUserModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        // Parametra për filtrim
        public string? Username { get; set; }
        public string? Email { get; set; }
        public bool? IsActive { get; set; }
        public int? RoleId { get; set; }
    }
}