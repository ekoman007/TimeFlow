using MediatR; 
using TimeFlow.Application.Features.Roles.DTOs;
using TimeFlow.Application.Features.UserDetails.DTOs;
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.UserDetails.Queries
{
    public class UserDetailsByIdQuery : IRequest<GeneralResponse<UserDetailsModel>>
    {
        public int Id { get; set; }
        public UserDetailsByIdQuery(int id)
        {
            Id = id;
        }
    }
}
