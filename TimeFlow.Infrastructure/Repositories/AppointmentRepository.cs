using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.Domain.Aggregates.Enums;
using TimeFlow.Domain.Repositories;
using TimeFlow.Infrastructure.Database;

namespace TimeFlow.Infrastructure.Repositories
{
    public class AppointmentRepository : GenericRepository<Appointment, int>, IAppointmentRepository
    {
        private readonly TimeFlowDbContext _dbContext;

        public AppointmentRepository(TimeFlowDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> GetAppointmentByNameAsync(int businessId, CancellationToken cancellationToken)
        {
            return await _dbContext.Appointments.AnyAsync(a => a.BusinessProfileId == businessId, cancellationToken);
        }

        public async Task<IEnumerable<Appointment>> GetByBusinessIdAsync(int businessId, DateTime? startDate = null, DateTime? endDate = null, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Appointments.Where(a => a.BusinessProfileId == businessId);

            if (startDate.HasValue)
            {
                query = query.Where(a => a.AppointmentDate >= startDate.Value.Date);
            }

            if (endDate.HasValue)
            {
                query = query.Where(a => a.AppointmentDate <= endDate.Value.Date);
            }

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Appointment>> GetByStaffIdAsync(int staffId, DateTime? startDate = null, DateTime? endDate = null, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Appointments.Where(a => a.StaffId == staffId);

            if (startDate.HasValue)
            {
                query = query.Where(a => a.AppointmentDate >= startDate.Value.Date);
            }

            if (endDate.HasValue)
            {
                query = query.Where(a => a.AppointmentDate <= endDate.Value.Date);
            }

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Appointment>> GetByUserDetailsIdAsync(int userDetailsId, DateTime? startDate = null, DateTime? endDate = null, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Appointments.Where(a => a.ApplicationUserDetailsId == userDetailsId);

            if (startDate.HasValue)
            {
                query = query.Where(a => a.AppointmentDate >= startDate.Value.Date);
            }

            if (endDate.HasValue)
            {
                query = query.Where(a => a.AppointmentDate <= endDate.Value.Date);
            }

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Appointment>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Appointments
                .Where(a => a.AppointmentDate >= startDate.Date && a.AppointmentDate <= endDate.Date)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> HasOverlappingAppointmentAsync(int staffId, DateTime date, TimeSpan startTime, TimeSpan endTime, int? excludeId = null, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Appointments
                .Where(a => a.StaffId == staffId 
                    && a.AppointmentDate.Date == date.Date
                    && a.Status != AppointmentStatus.Cancelled 
                    && a.Status != AppointmentStatus.NoShow);

            if (excludeId.HasValue)
            {
                query = query.Where(a => a.Id != excludeId.Value);
            }

            return await query.AnyAsync(a =>
                (startTime >= a.StartTime && startTime < a.EndTime) ||  // Start time falls within existing appointment
                (endTime > a.StartTime && endTime <= a.EndTime) ||      // End time falls within existing appointment
                (startTime <= a.StartTime && endTime >= a.EndTime),     // New appointment completely encompasses existing appointment
                cancellationToken);
        }

        // Legacy methods implementation
        public Task<Appointment> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException("Legacy method not implemented");
        }

        public Task<IEnumerable<Appointment>> GetByCustomerIdAsync(Guid customerId)
        {
            throw new NotImplementedException("Legacy method not implemented");
        }

        public Task<IEnumerable<Appointment>> GetByStaffIdAsync(Guid staffId, DateTime? startDate = null, DateTime? endDate = null)
        {
            throw new NotImplementedException("Legacy method not implemented");
        }

        public Task<IEnumerable<Appointment>> GetByBusinessIdAsync(Guid businessId, DateTime? startDate = null, DateTime? endDate = null)
        {
            throw new NotImplementedException("Legacy method not implemented");
        }

        public Task<IEnumerable<Appointment>> GetByStatusAsync(AppointmentStatus status)
        {
            throw new NotImplementedException("Legacy method not implemented");
        }

        public Task<Appointment> AddAsync(Appointment appointment)
        {
            throw new NotImplementedException("Legacy method not implemented");
        }

        public Task UpdateAsync(Appointment appointment)
        {
            throw new NotImplementedException("Legacy method not implemented");
        }

        public Task<bool> CheckOverlappingAppointmentsAsync(Guid staffId, DateTime startTime, DateTime endTime, Guid? excludeAppointmentId = null)
        {
            throw new NotImplementedException("Legacy method not implemented");
        }
    }
}
