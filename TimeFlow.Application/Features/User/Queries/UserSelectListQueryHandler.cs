using MediatR;
using Microsoft.EntityFrameworkCore; 
using TimeFlow.Application.Features.User.DTOs; 
using TimeFlow.Infrastructure.Contracts; 

namespace TimeFlow.Application.Features.User.Queries
{
    public class UserSelectListQueryHandler : IRequestHandler<UserSelectListQuery, List<ApplicationUserModelSelectList>>
    {
        private readonly IUserRepository _userRepository;

        public UserSelectListQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<ApplicationUserModelSelectList>> Handle(UserSelectListQuery query, CancellationToken cancellationToken)
        {


            var user = await _userRepository
             .GetQueryable(cancellationToken)
             .ToListAsync(cancellationToken);

            return user.Select(user => new ApplicationUserModelSelectList
            {
                Id = user.Id,
                Email = user.Email, 
            }).ToList();

        }
    }
}

