using MediatR;
using Microsoft.EntityFrameworkCore;
using TimeFlow.Application.Features.User.DTOs;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.User.Query
{
    public class UserListQueryHandler : IRequestHandler<UserListQuery, GeneralResponse<IEnumerable<UserModel>>>
    {
        private readonly IUserRepository _userRepository;

        public UserListQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GeneralResponse<IEnumerable<UserModel>>> Handle(UserListQuery query, CancellationToken cancellationToken = default)
        {

            IQueryable<ApplicationUser> queryable = _userRepository.Get(cancellationToken: cancellationToken);
             
            var totalCount = await queryable.CountAsync(cancellationToken);
             
            var users = await queryable
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize) 
                .ToListAsync(cancellationToken);

            var readModel = users.Select(x =>
                new UserModel
                {
                    Id = x.Id,
                    Username = x.Username,     
                    Email = x.Email,          
                    IsActive = x.IsActive,     
                    RoleId = x.RoleId,        
                });

            return new GeneralResponse<IEnumerable<UserModel>>
            {
                Success = true,
                Message = "User list fetched successfully",
                Result = readModel,
                TotalCount = totalCount,
                PageSize = query.PageSize,
                PageNumber = query.PageNumber,
                TotalPages = (int)Math.Ceiling((double)totalCount / query.PageSize)
            };
        }
    }
}
