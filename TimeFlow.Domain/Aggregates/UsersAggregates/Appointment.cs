using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.Domain.Aggregates.Enums;
using TimeFlow.SharedKernel;

namespace TimeFlow.Domain.Aggregates.UsersAggregates
{
    public class Appointment : AggregateRoot<int>
    {
        public int BusinessProfileId { get; set; }
        public BusinessProfile BusinessProfile { get; set; }

        public int? StaffId { get; set; }
        public Staff? Staff { get; set; }

        public int GuestId { get; set; }
        public Guest Guest { get; set; }

        public int ServiceId { get; set; }
        public Service Service { get; set; }

        public DateTime AppointmentDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
        public string? Notes { get; set; }

        public ICollection<AppointmentHistory> Histories { get; set; } = new List<AppointmentHistory>();

        public static Appointment Create(
            int businessProfileId,
            int guestId,
            int serviceId,
            DateTime appointmentDate,
            TimeSpan startTime,
            TimeSpan endTime,
            int? staffId = null,
            string? notes = null)
        {
            return new Appointment
            {
                BusinessProfileId = businessProfileId,
                GuestId = guestId,
                ServiceId = serviceId,
                AppointmentDate = appointmentDate,
                StartTime = startTime,
                EndTime = endTime,
                StaffId = staffId,
                Status = AppointmentStatus.Pending,
                Notes = notes
            };
        }

        public void Approve() => Status = AppointmentStatus.Approved;
        public void Cancel() => Status = AppointmentStatus.Canceled;
        public void Complete() => Status = AppointmentStatus.Completed;
    }

}
