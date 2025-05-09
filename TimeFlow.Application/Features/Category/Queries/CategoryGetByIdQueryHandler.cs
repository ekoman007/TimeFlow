using MediatR;
using TimeFlow.Application.Features.Category.DTOs;  
using TimeFlow.Application.Responses;
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.Category.Queries
{
    public class CategoryGetByIdQueryHandler : IRequestHandler<CategoryGetByIdQuery, GeneralResponse<CategoryModel>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryGetByIdQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<GeneralResponse<CategoryModel>> Handle(CategoryGetByIdQuery query, CancellationToken cancellationToken = default)
        {
            // Merrni rolin me ID nga repository
            var category = await _categoryRepository.GetByIdAsync(query.Id, cancellationToken).ConfigureAwait(false);


            if (category == null)
            {
                return new GeneralResponse<CategoryModel>
                {
                    Success = false,
                    Message = "Category not found"
                };
            }

            // Kthejeni në modelin e duhur
            var categoryModel = new CategoryModel
            { 
                Name = category.Name,
                Description = category.Description,
                Code = category.Code,
                IndustryId = category.IndustryId,
            };

            return new GeneralResponse<CategoryModel>
            {
                Success = true,
                Message = "Industry found successfully",
                Result = categoryModel
            };
        }
    }
}
