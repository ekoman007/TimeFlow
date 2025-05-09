using MediatR; 
using TimeFlow.Application.Features.Address.DTOs;
using TimeFlow.Application.Features.Services.DTOs;
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.Services.Queries
{
    public class ServiceByIdQuery : IRequest<GeneralResponse<ServiceModel>>
    {
        public int Id { get; set; }
        public ServiceByIdQuery(int id)
        {
            Id = id;
        }
    }
}
