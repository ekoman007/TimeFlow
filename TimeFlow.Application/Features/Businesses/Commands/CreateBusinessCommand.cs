using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TimeFlow.Application.DTOs;
using TimeFlow.Domain.Aggregates.Enums;

namespace TimeFlow.Application.Features.Businesses.Commands
{
    public class CreateBusinessCommand : IRequest<BusinessDto>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public BusinessCategory Category { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string TimeZone { get; set; }
        public string? LogoUrl { get; set; }
        public string? Website { get; set; }
        public string? PhoneNumber { get; set; }
    }

    public class CreateBusinessCommandHandler : IRequestHandler<CreateBusinessCommand, BusinessDto>
    {
        // Implementation will be added later
        public Task<BusinessDto> Handle(CreateBusinessCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
} 
