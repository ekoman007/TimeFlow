using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TimeFlow.Application.DTOs;

namespace TimeFlow.Application.Features.Businesses.Queries
{
    public class GetBusinessByIdQuery : IRequest<BusinessDto>
    {
        public Guid Id { get; set; }

        public GetBusinessByIdQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetBusinessByIdQueryHandler : IRequestHandler<GetBusinessByIdQuery, BusinessDto>
    {
        // Implementation will be added later
        public Task<BusinessDto> Handle(GetBusinessByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
} 
