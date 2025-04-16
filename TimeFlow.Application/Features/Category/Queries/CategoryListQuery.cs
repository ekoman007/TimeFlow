using MediatR;
using TimeFlow.Application.Features.Category.DTOs; 
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.Category.Queries
{
    public class CategoryListQuery : IRequest<GeneralResponse<IEnumerable<CategoryModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}