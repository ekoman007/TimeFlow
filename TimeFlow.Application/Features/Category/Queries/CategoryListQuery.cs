using MediatR;
using TimeFlow.Application.Features.Category.DTOs;
using TimeFlow.Application.Paged;
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.Category.Queries
{
    public class CategoryListQuery : IRequest<GeneralResponse<PagedResult<CategoryModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Name { get; set; } 
        public string? Code { get; set; }
    }
}