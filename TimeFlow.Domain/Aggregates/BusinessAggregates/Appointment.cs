using System;
using TimeFlow.Domain.Aggregates.Enums;
using TimeFlow.Domain.Aggregates.UsersAggregates;
using TimeFlow.SharedKernel;

namespace TimeFlow.Domain.Aggregates.BusinessAggregates
{
    public class Appointment : BaseEntity, IAggregateRoot
    {
        public Guid CustomerId { get; private set; }
        public User Customer { get; private set; }
        public Guid ServiceId { get; private set; }
        public Service Service { get; private set; }
        public Guid StaffMemberId { get; private set; }
        public StaffMember StaffMember { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public AppointmentStatus Status { get; private set; }
        public string? Notes { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        // Private constructor for EF Core
        private Appointment() { }

        public Appointment(Guid customerId, Guid serviceId, Guid staffMemberId, 
            DateTime startTime, DateTime endTime, string? notes = null)
        {
            if (startTime >= endTime)
                throw new ArgumentException("Start time must be earlier than end time");
            
            CustomerId = customerId;
            ServiceId = serviceId;
            StaffMemberId = staffMemberId;
            StartTime = startTime;
            EndTime = endTime;
            Status = AppointmentStatus.Pending;
            Notes = notes;
            CreatedAt = DateTime.UtcNow;
        }

        public void Confirm()
        {
            Status = AppointmentStatus.Confirmed;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Complete()
        {
            Status = AppointmentStatus.Completed;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Cancel()
        {
            Status = AppointmentStatus.Cancelled;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkNoShow()
        {
            Status = AppointmentStatus.NoShow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateNotes(string? notes)
        {
            Notes = notes;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Reschedule(DateTime newStartTime, DateTime newEndTime)
        {
            if (newStartTime >= newEndTime)
                throw new ArgumentException("Start time must be earlier than end time");
            
            if (Status == AppointmentStatus.Completed || Status == AppointmentStatus.Cancelled || Status == AppointmentStatus.NoShow)
                throw new InvalidOperationException("Cannot reschedule a completed, cancelled, or no-show appointment");
            
            StartTime = newStartTime;
            EndTime = newEndTime;
            UpdatedAt = DateTime.UtcNow;
        }
    }
} 