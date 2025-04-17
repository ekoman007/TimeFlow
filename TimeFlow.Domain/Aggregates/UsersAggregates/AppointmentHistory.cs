 
using TimeFlow.Domain.Aggregates.Enums;
using TimeFlow.SharedKernel;

namespace TimeFlow.Domain.Aggregates.UsersAggregates
{
    public class AppointmentHistory : AggregateRoot<int>
    {
        public int StaffId { get; set; }
        public Staff Staff { get; set; }

        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }

        public DateTime AppointmentDate { get; set; }

        // Replace individual guest fields with a foreign key to the Guest entity
        public int GuestId { get; set; }
        public Guest Guest { get; set; }  // Navigation property

        public AppointmentStatus Status { get; set; }
    }


}
