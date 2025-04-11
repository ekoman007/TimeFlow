using MediatR;
using Microsoft.EntityFrameworkCore;
using TimeFlow.Application.Features.User.DTOs;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.User.Query
{
    public class UserListQueryHandler : IRequestHandler<UserListQuery, GeneralResponse<IEnumerable<ApplicationUserModel>>>
    {
        private readonly IUserRepository _userRepository;

        public UserListQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GeneralResponse<IEnumerable<ApplicationUserModel>>> Handle(UserListQuery query, CancellationToken cancellationToken = default)
        {
            // Krijo një queryable që mund të filtrohet
            IQueryable<ApplicationUser> queryable = _userRepository.Get(cancellationToken: cancellationToken);

            // Filtrimi për Username
            if (!string.IsNullOrEmpty(query.Username))
            {
                queryable = queryable.Where(u => u.Username.Contains(query.Username));
            }

            // Filtrimi për Email
            if (!string.IsNullOrEmpty(query.Email))
            {
                queryable = queryable.Where(u => u.Email.Contains(query.Email));
            }

            // Filtrimi për IsActive
            if (query.IsActive.HasValue)
            {
                queryable = queryable.Where(u => u.IsActive == query.IsActive.Value);
            }

            // Filtrimi për RoleId
            if (query.RoleId.HasValue)
            {
                queryable = queryable.Where(u => u.RoleId == query.RoleId.Value);
            }
             


           // IQueryable<ApplicationUser> queryable = _userRepository.Get(cancellationToken: cancellationToken);
             
            var totalCount = await queryable.CountAsync(cancellationToken);
             
            var users = await queryable
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize) 
                .ToListAsync(cancellationToken);

            var readModel = users.Select(x =>
                new ApplicationUserModel
                {
                    Id = x.Id,
                    Username = x.Username,     
                    Email = x.Email,          
                    IsActive = x.IsActive,     
                    RoleId = x.RoleId,        
                });

            return new GeneralResponse<IEnumerable<ApplicationUserModel>>
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
