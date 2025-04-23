 
using TimeFlow.SharedKernel;

namespace TimeFlow.Domain.Aggregates.UsersAggregates
{
    public class BusinessProfile : AggregateRoot<int>
    {
        public string BusinessName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Website { get; set; }
        public string NIPT { get; set; }
        public string Description { get; set; }
        public string LogoUrl { get; set; }
        public int IndustryId { get; set; }
        public Industry Industries { get; set; } 
        public int UserDetailsId { get; set; }    
        public ApplicationUserDetails UserDetails { get; set; }
        public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
        public ICollection<Service> Services { get; set; } = new List<Service>();

        public static BusinessProfile Create(string businessName,
            string email, string phoneNumber, string website,
            string? description, string logoUrl, int industryId,
            int userDetailsId)
        {
            var businessProfile = new BusinessProfile
            {
                BusinessName = businessName,
                Email = email,
                PhoneNumber = phoneNumber,
                Website = website,
                Description = description,
                LogoUrl = logoUrl,
                IndustryId = industryId,
                UserDetailsId = userDetailsId,

            };

            businessProfile.ValidateBusinessProfile();

            return businessProfile;
        }

        public void ChangeBussinesName(string businessName)
        {
            BusinessName = businessName;
            ValidateBusinessProfile();
        }

        public void ChangeEmail(string email)
        {
            Email = email;
            ValidateBusinessProfile();
        }

        public void ChangePhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
            ValidateBusinessProfile();
        }

        public void ChangeWebsite(string website)
        {
            Website = website;
            ValidateBusinessProfile();
        }  
        
        public void ChangeDescription(string description)
        {
            Description = description;
            ValidateBusinessProfile();
        } 
        public void ChangeLogoUrl(string logoUrl)
        {
            LogoUrl = logoUrl;
            ValidateBusinessProfile();
        } 
        
        public void ChangeIndustry(int industryId)
        {
            IndustryId = industryId;
            ValidateBusinessProfile();
        }
        
        public void ChangeUserDetails(int userId)
        {
            UserDetailsId = userId;
            ValidateBusinessProfile();
        }
        public void ChangeToActive()
        {
            IsActive = true;
            ValidateBusinessProfile();
        }

        public void ChangeToDeActive()
        {
            IsActive = false;
            ValidateBusinessProfile();
        }

        public void ChangeStatusToActive()
        {
            Status = EntityStatus.Active;
            ValidateBusinessProfile();
        }
        public void ChangeStatusToDelete()
        {
            Status = EntityStatus.Deleted;
            ValidateBusinessProfile();
        }
        private void ValidateBusinessProfile()
        {
            if (string.IsNullOrWhiteSpace(LogoUrl))
                ThrowDomainException("LogoUrl is required.");

            if (string.IsNullOrWhiteSpace(Description))
                ThrowDomainException("Description is required.");

            if (string.IsNullOrWhiteSpace(Website))
                ThrowDomainException("Website is required.");


            if (string.IsNullOrWhiteSpace(PhoneNumber))
                ThrowDomainException("PhoneNumber is required.");
            
            if (string.IsNullOrWhiteSpace(Email))
                ThrowDomainException("Email is required.");
            
            if (string.IsNullOrWhiteSpace(BusinessName))
                ThrowDomainException("BusinessName is required.");
        }
    }

} 