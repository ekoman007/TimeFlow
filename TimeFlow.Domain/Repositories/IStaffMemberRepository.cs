using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeFlow.Domain.Aggregates.BusinessAggregates;

namespace TimeFlow.Domain.Repositories
{
    public interface IStaffMemberRepository
    {
        Task<StaffMember> GetByIdAsync(Guid id);
        Task<StaffMember> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<StaffMember>> GetByBusinessIdAsync(Guid businessId);
        Task<IEnumerable<StaffMember>> GetByServiceIdAsync(Guid serviceId);
        Task<StaffMember> AddAsync(StaffMember staffMember);
        Task UpdateAsync(StaffMember staffMember);
        Task AddServiceAsync(StaffService staffService);
        Task AddAvailabilityAsync(StaffAvailability availability);
        Task<IEnumerable<StaffAvailability>> GetAvailabilitiesByStaffIdAsync(Guid staffId);
        Task UpdateAvailabilityAsync(StaffAvailability availability);
    }
} 