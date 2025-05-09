using MediatR;
using TimeFlow.Application.Features.BussinesProfile.DTOs;
using TimeFlow.Application.Features.BussinesProfile.Queries;
using TimeFlow.Application.Paged;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Aggregates.BusinessAggregates;
using TimeFlow.Infrastructure.Contracts;

public class BusinessProfileListQueryHandler : IRequestHandler<BusinessProfileListQuery, GeneralResponse<PagedResult<BussinesProfileModel>>>
{
    private readonly IBussinesProfileRepository _bussinesProfileRepository;

    public BusinessProfileListQueryHandler(IBussinesProfileRepository bussinesProfileRepository)
    {
        _bussinesProfileRepository = bussinesProfileRepository;
    }

    public async Task<GeneralResponse<PagedResult<BussinesProfileModel>>> Handle(BusinessProfileListQuery query, CancellationToken cancellationToken = default)
    {
        var queryable = _bussinesProfileRepository.GetQueryable(cancellationToken);

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
                UserDetailsId = x.UserDetailsId,
                IndustryId = x.IndustryId
            },
            cancellationToken
        );

        return new GeneralResponse<PagedResult<BussinesProfileModel>>
        {
            Success = true,
            Message = "Business profile list.",
            Result = pagedResult
        };
    }
}


