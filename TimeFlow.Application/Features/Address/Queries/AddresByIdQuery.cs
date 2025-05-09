using MediatR; 
using TimeFlow.Application.Features.Address.DTOs; 
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.Address.Queries
{
    public class AddresByIdQuery : IRequest<GeneralResponse<AddressModel>>
    {
        public int Id { get; set; }
        public AddresByIdQuery(int id)
        {
            Id = id;
        }
    }
}
