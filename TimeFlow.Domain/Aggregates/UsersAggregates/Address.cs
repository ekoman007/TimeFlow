using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.SharedKernel;

namespace TimeFlow.Domain.Aggregates.UsersAggregates
{
    public class Address : AggregateRoot<int>
    {
        public int Id { get; set; }   
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string? Latitude { get; set; }  
        public string? Longitude { get; set; }  
        public bool IsPrimary { get; set; }    
        public int? ApplicationUserDetailsId { get; set; }
        public virtual ApplicationUserDetails? ApplicationUserDetails { get; set; }

        public int? BusinessProfileId { get; set; }
        public virtual BusinessProfile? BusinessProfile { get; set; }

        public static Address Create(string street,
                    string city, string country, string zipCode, 
                    string? latitude,string longitude,bool isPrimary, 
                    int applicationUserDetailsId, int businessProfileId)
        {
            var address = new Address
            {
                Street = street,
                City = city,
                Country = country,
                ZipCode = zipCode,
                Latitude = latitude,
                Longitude = longitude,
                IsPrimary = isPrimary,
                ApplicationUserDetailsId = applicationUserDetailsId,
                BusinessProfileId = businessProfileId

            };

            address.ValidateAddress();

            return address;
        }

        public void ChangeStreet(string street)
        {
            Street = street;
            ValidateAddress();
        }

        public void ChangeCity(string city)
        {
            City = city;
            ValidateAddress();
        }

        public void ChangeCountry(string country)
        {
            Country = country;
            ValidateAddress();
        }

        public void ChangeZipCode(string zipcode)
        {
            ZipCode = zipcode;
            ValidateAddress();
        }
        public void ChangeToActive()
        {
            IsActive = true;
            ValidateAddress();
        }

        public void ChangeToDeActive()
        {
            IsActive = false;
            ValidateAddress();
        }

        public void ChangeStatusToActive()
        {
            Status = EntityStatus.Active;
            ValidateAddress();
        }
        public void ChangeStatusToDelete()
        {
            Status = EntityStatus.Deleted;
            ValidateAddress();
        }
        private void ValidateAddress()
        {
            if (string.IsNullOrWhiteSpace(Street))
                ThrowDomainException("Street is required.");

            if (string.IsNullOrWhiteSpace(City))
                ThrowDomainException("City is required.");
            
            if (string.IsNullOrWhiteSpace(Country))
                ThrowDomainException("Country is required.");
             

            if (string.IsNullOrWhiteSpace(ZipCode))
                ThrowDomainException("ZipCode is required.");
        }
    }

}
 