using MediatR;
using Microsoft.EntityFrameworkCore;
using TimeFlow.Application.Features.Industry.DTOs;
using TimeFlow.Application.Features.Industry.Queris;
using TimeFlow.Application.Paged;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Infrastructure.Contracts;
using TimeFlow.Infrastructure.Repositories;

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


        // Filtrimet
        if (!string.IsNullOrEmpty(query.Name))
        {
            queryable = queryable.Where(u => u.Name.Contains(query.Name));
        }

        if (!string.IsNullOrEmpty(query.Code))
        {
            queryable = queryable.Where(u => u.Code.Contains(query.Code));
        }
         

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
