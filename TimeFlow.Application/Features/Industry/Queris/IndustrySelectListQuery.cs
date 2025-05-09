using MediatR; 
using TimeFlow.Application.Features.Industry.DTOs; 

namespace TimeFlow.Application.Features.Industry.Queris
{
     public class IndustrySelectListQuery : IRequest<List<IndustrySelectListModel>> { }
     
}

