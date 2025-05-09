using System;
using TimeFlow.SharedKernel;

namespace TimeFlow.Domain.Aggregates.BusinessAggregates
{
    public class StaffAvailability : BaseEntity
    {
        public Guid StaffMemberId { get; private set; }
        public StaffMember StaffMember { get; private set; }
        public DayOfWeek DayOfWeek { get; private set; }
        public TimeSpan StartTime { get; private set; }
        public TimeSpan EndTime { get; private set; }
        public bool IsRecurring { get; private set; }
        public DateTime? SpecificDate { get; private set; }

        // Private constructor for EF Core
        private StaffAvailability() { }

        public StaffAvailability(Guid staffMemberId, DayOfWeek dayOfWeek, 
            TimeSpan startTime, TimeSpan endTime, bool isRecurring, DateTime? specificDate = null)
        {
            if (startTime >= endTime)
                throw new ArgumentException("Start time must be earlier than end time");
            
            StaffMemberId = staffMemberId;
            DayOfWeek = dayOfWeek;
            StartTime = startTime;
            EndTime = endTime;
            IsRecurring = isRecurring;
            
            if (isRecurring && specificDate.HasValue)
                throw new ArgumentException("Specific date should be null for recurring availabilities");
            
            SpecificDate = specificDate;
        }

        public void UpdateTime(TimeSpan startTime, TimeSpan endTime)
        {
            if (startTime >= endTime)
                throw new ArgumentException("Start time must be earlier than end time");
            
            StartTime = startTime;
            EndTime = endTime;
        }
    }
} 