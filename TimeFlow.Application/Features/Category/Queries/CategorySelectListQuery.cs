using MediatR;
using TimeFlow.Application.Features.Category.DTOs; 

namespace TimeFlow.Application.Features.Category.Queries
{
    public class CategorySelectListQuery : IRequest<List<CategorySelectListModel>> { }
} 
