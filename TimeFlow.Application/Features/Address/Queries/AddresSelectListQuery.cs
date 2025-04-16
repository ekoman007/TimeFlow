using MediatR; 
using TimeFlow.Application.Features.Address.DTOs; 

namespace TimeFlow.Application.Features.Address.Queries
{
    public class AddresSelectListQuery : IRequest<List<AddresSelectListModal>> { }
 
}
