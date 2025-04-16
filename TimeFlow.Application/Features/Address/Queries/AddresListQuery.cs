using MediatR;
using TimeFlow.Application.Features.Address.DTOs; 
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.Address.Queries
{
    public class AddresListQuery : IRequest<GeneralResponse<IEnumerable<AddressModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}