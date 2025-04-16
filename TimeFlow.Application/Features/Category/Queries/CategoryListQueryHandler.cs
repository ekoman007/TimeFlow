using MediatR;
using Microsoft.EntityFrameworkCore; 
using TimeFlow.Application.Features.Category.DTOs; 
using TimeFlow.Application.Responses;
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.Category.Queries
{
    public class CategoryListQueryHandler : IRequestHandler<CategoryListQuery, GeneralResponse<IEnumerable<CategoryModel>>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryListQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<GeneralResponse<IEnumerable<CategoryModel>>> Handle(CategoryListQuery query, CancellationToken cancellationToken = default)
        {
            IQueryable<Domain.Aggregates.UsersAggregates.Category> queryable = _categoryRepository.Get(cancellationToken: cancellationToken);

            // Për aplikimin e paginimit
            var totalCount = await queryable.CountAsync(cancellationToken);
            var roles = await queryable
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync(cancellationToken);

            var categoryModel = roles.Select(x =>
                new CategoryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Code = x.Code,
                    IndustryId = x.IndustryId,
                });

            return new GeneralResponse<IEnumerable<CategoryModel>>
            {
                Success = true,
                Message = "Category list.",
                Result = categoryModel,
                TotalCount = totalCount,
                PageSize = query.PageSize,
                PageNumber = query.PageNumber,
                TotalPages = (int)Math.Ceiling((double)totalCount / query.PageSize)
            };
        }

    }
}