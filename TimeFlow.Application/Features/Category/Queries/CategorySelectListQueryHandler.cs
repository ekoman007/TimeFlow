using MediatR;
using Microsoft.EntityFrameworkCore;
using TimeFlow.Application.Features.Category.DTOs;  
using TimeFlow.Infrastructure.Contracts;

namespace TimeFlow.Application.Features.Category.Queries
{
    public class CategorySelectListQueryHandler : IRequestHandler<CategorySelectListQuery, List<CategorySelectListModel>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategorySelectListQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategorySelectListModel>> Handle(CategorySelectListQuery query, CancellationToken cancellationToken)
        {
            // Merrni rolet nga repository

            var categoryList = await _categoryRepository
                .GetQueryable(cancellationToken)
                .ToListAsync(cancellationToken);

            // Kthejeni vetëm Id dhe RoleName për çdo rol
            return categoryList.Select(x => new CategorySelectListModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }
    }
}

