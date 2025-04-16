 
using TimeFlow.SharedKernel;

namespace TimeFlow.Domain.Aggregates.UsersAggregates
{
    public class ApplicationUserDetails : AggregateRoot<int>
    {

        public string FullName { get; set; } 
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string ProfilePicture { get; set; }
        public int UserId { get; set; } 
        public virtual ICollection<BusinessProfile> BusinessProfile { get; private set; } = new List<BusinessProfile>();
        public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

        public static ApplicationUserDetails Create(string fullname, 
                            string phonenumber, DateTime? dateOfBrith, 
                            string profilePictures, int userId)
        {
            var userDetails = new ApplicationUserDetails
            {
                FullName = fullname, 
                PhoneNumber = phonenumber,
                DateOfBirth = dateOfBrith,
                ProfilePicture = profilePictures,
                UserId = userId,
            };

            userDetails.ValidateUserDetails();

            return userDetails;
        }

        public void ChangeFullName(string fullname)
        {
            FullName = fullname;
            ValidateUserDetails();
        }

        public void ChangePhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
            ValidateUserDetails();
        }

        public void ChangeDateOfBirth(DateTime? dateOfBirth)
        {
            DateOfBirth = dateOfBirth;
            ValidateUserDetails();
        }

        public void ChangeProfilePicture(string profilePicture)
        {
            ProfilePicture = profilePicture;
            ValidateUserDetails();
        } 
        public void ChangeToActive()
        {
            IsActive =true;
            ValidateUserDetails();
        }

        public void ChangeToDeActive()
        {
            IsActive = false;
            ValidateUserDetails();
        }

        public void ChangeStatusToActive()
        {
            Status = EntityStatus.Active;
            ValidateUserDetails();
        }
        public void ChangeStatusToDelete()
        {
            Status = EntityStatus.Deleted;
            ValidateUserDetails();
        }
        private void ValidateUserDetails()
        {
            if (string.IsNullOrWhiteSpace(FullName))
                ThrowDomainException("FullName is required."); 

            if (string.IsNullOrWhiteSpace(PhoneNumber))
                ThrowDomainException("PhoneNumber is required.");

            if (DateOfBirth == null)
                ThrowDomainException("DateOfBirth is required.");

            if (string.IsNullOrWhiteSpace(ProfilePicture))
                ThrowDomainException("ProfilePicture is required.");
        }
    }

}
