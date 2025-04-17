using MediatR;
using TimeFlow.Application.Features.Industry.DTOs;
using TimeFlow.Application.Features.Industry.Queris;
using TimeFlow.Application.Paged;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Infrastructure.Contracts;

public class IndustryListQueryHandler : IRequestHandler<IndustryListQuery, GeneralResponse<PagedResult<IndustryModel>>>
{
    private readonly IIndustryRepository _industryRepository;

    public IndustryListQueryHandler(IIndustryRepository industryRepository)
    {
        _industryRepository = industryRepository;
    }

    public async Task<GeneralResponse<PagedResult<IndustryModel>>> Handle(IndustryListQuery query, CancellationToken cancellationToken = default)
    {
        IQueryable<Industry> queryable = _industryRepository.Get(cancellationToken: cancellationToken);

        // Paginimi dhe mapping me ToPagedResultAsync
        var pagedResult = await queryable.ToPagedResultAsync(
            query.PageNumber,
            query.PageSize,
            x => new IndustryModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Code = x.Code,
                ModifiedOn = x.ModifiedOn
            },
            cancellationToken
        );

        return new GeneralResponse<PagedResult<IndustryModel>>
        {
            Success = true,
            Message = "Industry list.",
            Result = pagedResult
        };
    }
}
