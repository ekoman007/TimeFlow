using MediatR;
using TimeFlow.Application.Features.Address.DTOs;
using TimeFlow.Application.Paged;
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.Address.Queries
{
    public class AddresListQuery : IRequest<GeneralResponse<PagedResult<AddressModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}