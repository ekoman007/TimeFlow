using MediatR; 
using TimeFlow.Application.Features.User.DTOs;

namespace TimeFlow.Application.Features.User.Queries
{
    public class UserSelectListQuery : IRequest<List<ApplicationUserModelSelectList>> { }
     
}
