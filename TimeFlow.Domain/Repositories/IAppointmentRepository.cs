using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TimeFlow.Domain.Aggregates.UsersAggregates;

namespace TimeFlow.Domain.Repositories
{
    public interface IAppointmentRepository : IRepository<Appointment, int>
    {
        Task<bool> GetAppointmentByNameAsync(int businessId, CancellationToken cancellationToken);
        Task<IEnumerable<Appointment>> GetByBusinessIdAsync(int businessId, DateTime? startDate = null, DateTime? endDate = null, CancellationToken cancellationToken = default);
        Task<IEnumerable<Appointment>> GetByStaffIdAsync(int staffId, DateTime? startDate = null, DateTime? endDate = null, CancellationToken cancellationToken = default);
        Task<IEnumerable<Appointment>> GetByUserDetailsIdAsync(int userDetailsId, DateTime? startDate = null, DateTime? endDate = null, CancellationToken cancellationToken = default);
        Task<IEnumerable<Appointment>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        Task<bool> HasOverlappingAppointmentAsync(int staffId, DateTime date, TimeSpan startTime, TimeSpan endTime, int? excludeId = null, CancellationToken cancellationToken = default);
    }
} 