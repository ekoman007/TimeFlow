using MediatR;
using TimeFlow.Application.Features.Staffs.DTOs;
using TimeFlow.Application.Features.Staffs.Queries;
using TimeFlow.Application.Paged;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Infrastructure.Contracts;

public class StaffListQueryHandler : IRequestHandler<StaffListQuery, GeneralResponse<PagedResult<StaffModel>>>
{
    private readonly IStaffRepository _staffRepository;

    public StaffListQueryHandler(IStaffRepository staffRepository)
    {
        _staffRepository = staffRepository;
    }

    public async Task<GeneralResponse<PagedResult<StaffModel>>> Handle(StaffListQuery query, CancellationToken cancellationToken = default)
    {
        var queryable = _staffRepository.GetQueryable(cancellationToken);

        var pagedResult = await queryable.ToPagedResultAsync(
            query.PageNumber,
            query.PageSize,
            x => new StaffModel
            {
                Id = x.Id,
                BusinessProfileId = x.BusinessProfileId,
                FullName = x.FullName,
                PhoneNumber = x.PhoneNumber,
                Email = x.Email,
                RoleId = x.RoleId
            },
            cancellationToken
        );

        return new GeneralResponse<PagedResult<StaffModel>>
        {
            Success = true,
            Message = "Staff list.",
            Result = pagedResult
        };
    }
}


