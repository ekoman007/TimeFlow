using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TimeFlow.Application.DTOs;

namespace TimeFlow.Application.Features.StaffMembers.Queries
{
    public class GetStaffByBusinessQuery : IRequest<List<StaffMemberDto>>
    {
        public Guid BusinessId { get; set; }

        public GetStaffByBusinessQuery(Guid businessId)
        {
            BusinessId = businessId;
        }
    }

    public class GetStaffByBusinessQueryHandler : IRequestHandler<GetStaffByBusinessQuery, List<StaffMemberDto>>
    {
        // Implementation will be added later
        public Task<List<StaffMemberDto>> Handle(GetStaffByBusinessQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
} 
