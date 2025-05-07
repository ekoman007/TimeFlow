using MediatR;
using TimeFlow.Application.Features.Category.DTOs;
using TimeFlow.Application.Features.Category.Queries;
using TimeFlow.Application.Paged;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Infrastructure.Contracts;
using TimeFlow.Infrastructure.Repositories;

public class CategoryListQueryHandler : IRequestHandler<CategoryListQuery, GeneralResponse<PagedResult<CategoryModel>>>
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryListQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<GeneralResponse<PagedResult<CategoryModel>>> Handle(CategoryListQuery query, CancellationToken cancellationToken = default)
    {

        IQueryable<Category> queryable = _categoryRepository.Get(cancellationToken: cancellationToken);


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
            x => new CategoryModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Code = x.Code,
                IndustryId = x.IndustryId
            },
            cancellationToken
        );

        return new GeneralResponse<PagedResult<CategoryModel>>
        {
            Success = true,
            Message = "Category list.",
            Result = pagedResult
        };
    }
}
