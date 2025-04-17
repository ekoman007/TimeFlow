using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.SharedKernel;

namespace TimeFlow.Domain.Aggregates.UsersAggregates
{
    public class Guest : AggregateRoot<int>
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

        // This will allow easy access to the appointment history from the Guest entity
        public ICollection<AppointmentHistory> AppointmentHistories { get; set; } = new List<AppointmentHistory>();

        public static Guest Create(string fullName, string phoneNumber, string email)
        {
            return new Guest
            {
                FullName = fullName,
                PhoneNumber = phoneNumber,
                Email = email
            };
        }
    }


}
