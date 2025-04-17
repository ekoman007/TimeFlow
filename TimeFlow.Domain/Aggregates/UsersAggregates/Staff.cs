using System.Diagnostics;
using System.Xml.Linq;
using TimeFlow.SharedKernel;

namespace TimeFlow.Domain.Aggregates.UsersAggregates
{
    public class Staff : AggregateRoot<int>
    {
        public int BusinessProfileId { get; set; }
        public BusinessProfile BusinessProfile { get; set; }

        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public ICollection<WorkSchedule> WorkSchedules { get; set; } = new List<WorkSchedule>();
        public ICollection<AppointmentHistory> AppointmentHistories { get; set; } = new List<AppointmentHistory>();


        public static Staff Create(int businessProfileId, string fullname, string phoneNumber,
                                     string email, int roleId)
        {
            var staff = new Staff
            {
                BusinessProfileId = businessProfileId,
                FullName = fullname,
                PhoneNumber = phoneNumber,
                Email = email,
                RoleId = roleId
            };

            staff.ValidateStaff();

            return staff;
        }

        public void ChangeToActive()
        {
            IsActive = true;
            ValidateStaff();
        }
        public void ChangeToDelete()
        {
            IsActive = false;
            ValidateStaff();
        }

        public void ChangeStaff(int businessProfileId, string fullname, 
                                string phoneNumber,string email, int roleId)
            {
                BusinessProfileId = businessProfileId;
                FullName = fullname;
                PhoneNumber = phoneNumber;
                Email = email;
                RoleId = roleId;

                ValidateStaff();
            } 

        private void ValidateStaff()
        {
            if (BusinessProfileId == null || BusinessProfileId <= 0)
                ThrowDomainException("BusinessProfileId is required.");

            if (string.IsNullOrWhiteSpace(FullName))
                ThrowDomainException("FullName is required.");
            
            if (string.IsNullOrWhiteSpace(PhoneNumber))
                ThrowDomainException("PhoneNumber is required.");
            
            if (string.IsNullOrWhiteSpace(Email))
                ThrowDomainException("Email is required."); 
             

        }
    }
}
