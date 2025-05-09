using System;
using TimeFlow.SharedKernel;

namespace TimeFlow.Domain.Aggregates.BusinessAggregates
{
    public class StaffService : BaseEntity
    {
        public Guid StaffMemberId { get; private set; }
        public StaffMember StaffMember { get; private set; }
        public Guid ServiceId { get; private set; }
        public Service Service { get; private set; }

        // Private constructor for EF Core
        private StaffService() { }

        public StaffService(Guid staffMemberId, Guid serviceId)
        {
            StaffMemberId = staffMemberId;
            ServiceId = serviceId;
        }
    }
} 