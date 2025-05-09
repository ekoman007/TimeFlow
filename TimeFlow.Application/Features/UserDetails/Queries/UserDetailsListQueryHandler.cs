using MediatR;
using TimeFlow.Application.Features.UserDetails.DTOs;
using TimeFlow.Application.Features.UserDetails.Queries;
using TimeFlow.Application.Paged;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Infrastructure.Contracts;

public class UserDetailsListQueryHandler : IRequestHandler<UserDetailsListQuery, GeneralResponse<PagedResult<UserDetailsModel>>>
{
    private readonly IUserDetailsRepository _userDetailsRepository;

    public UserDetailsListQueryHandler(IUserDetailsRepository userDetailsRepository)
    {
        _userDetailsRepository = userDetailsRepository;
    }

    public async Task<GeneralResponse<PagedResult<UserDetailsModel>>> Handle(UserDetailsListQuery query, CancellationToken cancellationToken = default)
    {
        var queryable = _userDetailsRepository.GetQueryable(cancellationToken);

        var pagedResult = await queryable.ToPagedResultAsync(
            query.PageNumber,
            query.PageSize,
            x => new UserDetailsModel
            {
                Id = x.Id,
                FullName = x.FullName,
                PhoneNumber = x.PhoneNumber,
                DateOfBirth = x.DateOfBirth,
                ProfilePicture = x.ProfilePicture,
                UserId = x.UserId
            },
            cancellationToken
        );

        return new GeneralResponse<PagedResult<UserDetailsModel>>
        {
            Success = true,
            Message = "UserDetail list.",
            Result = pagedResult
        };
    }
}


