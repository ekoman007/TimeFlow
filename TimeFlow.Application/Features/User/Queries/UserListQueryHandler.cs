using MediatR;
using TimeFlow.Application.Features.User.DTOs;
using TimeFlow.Application.Features.User.Query;
using TimeFlow.Application.Paged;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Infrastructure.Contracts;

public class UserListQueryHandler : IRequestHandler<UserListQuery, GeneralResponse<PagedResult<ApplicationUserModel>>>
{
    private readonly IUserRepository _userRepository;

    public UserListQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<GeneralResponse<PagedResult<ApplicationUserModel>>> Handle(UserListQuery query, CancellationToken cancellationToken = default)
    {
        IQueryable<ApplicationUser> queryable = _userRepository.Get(cancellationToken: cancellationToken);

        // Filtrimet
        if (!string.IsNullOrEmpty(query.Username))
        {
            queryable = queryable.Where(u => u.Username.Contains(query.Username));
        }

        if (!string.IsNullOrEmpty(query.Email))
        {
            queryable = queryable.Where(u => u.Email.Contains(query.Email));
        }

        if (query.IsActive.HasValue)
        {
            queryable = queryable.Where(u => u.IsActive == query.IsActive.Value);
        }

        if (query.RoleId.HasValue)
        {
            queryable = queryable.Where(u => u.RoleId == query.RoleId.Value);
        }

        // Paginimi + mapping
        var pagedResult = await queryable.ToPagedResultAsync(
            query.PageNumber,
            query.PageSize,
            x => new ApplicationUserModel
            {
                Id = x.Id,
                Username = x.Username,
                Email = x.Email,
                IsActive = x.IsActive,
                RoleId = x.RoleId
            },
            cancellationToken
        );

        return new GeneralResponse<PagedResult<ApplicationUserModel>>
        {
            Success = true,
            Message = "User list fetched successfully",
            Result = pagedResult
        };
    }
}
