using MediatR;
using TimeFlow.Application.Features.Category.DTOs; 
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.Category.Queries
{
    public class CategoryGetByIdQuery : IRequest<GeneralResponse<CategoryModel>>
    {
        public int Id { get; set; }
        public CategoryGetByIdQuery(int id)
        {
            Id = id;
        }
    }
}

