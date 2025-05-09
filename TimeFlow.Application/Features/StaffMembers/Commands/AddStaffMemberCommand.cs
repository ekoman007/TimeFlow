using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TimeFlow.Application.DTOs;

namespace TimeFlow.Application.Features.StaffMembers.Commands
{
    public class AddStaffMemberCommand : IRequest<StaffMemberDto>
    {
        public Guid UserId { get; set; }
        public Guid BusinessId { get; set; }
        public string Title { get; set; }
        public string? Bio { get; set; }
        public string? ProfileImageUrl { get; set; }
    }

    public class AddStaffMemberCommandHandler : IRequestHandler<AddStaffMemberCommand, StaffMemberDto>
    {
        // Implementation will be added later
        public Task<StaffMemberDto> Handle(AddStaffMemberCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
} 
