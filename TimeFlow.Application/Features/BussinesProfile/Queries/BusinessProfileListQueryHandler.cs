using MediatR;
using TimeFlow.Application.Features.BussinesProfile.DTOs;
using TimeFlow.Application.Features.BussinesProfile.Queries;
using TimeFlow.Application.Paged;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Infrastructure.Contracts;

public class BusinessProfileListQueryHandler : IRequestHandler<BusinessProfileListQuery, GeneralResponse<PagedResult<BussinesProfileModel>>>
{
    private readonly IBussinesProfileRepository _bussinessProfileRepository;

    public BusinessProfileListQueryHandler(IBussinesProfileRepository bussinessProfileRepository)
    {
        _bussinessProfileRepository = bussinessProfileRepository;
    }

    public async Task<GeneralResponse<PagedResult<BussinesProfileModel>>> Handle(BusinessProfileListQuery query, CancellationToken cancellationToken = default)
    {
        IQueryable<BusinessProfile> queryable = _bussinessProfileRepository.Get(cancellationToken: cancellationToken);

        // Paginimi dhe mapping me ToPagedResultAsync
        var pagedResult = await queryable.ToPagedResultAsync(
            query.PageNumber,
            query.PageSize,
            x => new BussinesProfileModel
            {
                Id = x.Id,
                BusinessName = x.BusinessName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                Website = x.Website,
                Description = x.Description,
                LogoUrl = x.LogoUrl,
                IndustryId = x.IndustryId,
                UserDetailsId = x.UserDetailsId
            },
            cancellationToken
        );

        return new GeneralResponse<PagedResult<BussinesProfileModel>>
        {
            Success = true,
            Message = "Bussines profile list.",
            Result = pagedResult
        };
    }
}
