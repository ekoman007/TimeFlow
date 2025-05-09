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

        public int? ApplicationUserDetailsId { get; set; }
        public ApplicationUserDetails? ApplicationUserDetails { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }

        public DateTime AppointmentDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
        public string? Notes { get; set; }

        public ICollection<AppointmentHistory> Histories { get; set; } = new List<AppointmentHistory>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();

        public static Appointment Create(
            int businessProfileId,
            int? staffId,
            int guestId,
            int applicationUserDetailsId,
            int serviceId,
            DateTime appointmentDate,
            TimeSpan startTime,
            TimeSpan endTime,
            string? notes = null)
        {
            return new Appointment
            {
                BusinessProfileId = businessProfileId,
                StaffId = staffId,
                GuestId = guestId,
                ApplicationUserDetailsId = applicationUserDetailsId,
                ServiceId = serviceId,
                AppointmentDate = appointmentDate,
                StartTime = startTime,
                EndTime = endTime,
                Status = AppointmentStatus.Pending,
                Notes = notes
            };
        }

        public void Confirm() => Status = AppointmentStatus.Confirmed;
        public void Cancel() => Status = AppointmentStatus.Cancelled;
        public void Complete() => Status = AppointmentStatus.Completed;
    }
}