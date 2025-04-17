

using TimeFlow.SharedKernel;

namespace TimeFlow.Domain.Aggregates.UsersAggregates
{
    public class Service : AggregateRoot<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int DurationInMinutes { get; set; }
        public string? ServiceType { get; set; } /*"standard", "premium", "express"*/
        public string? Tags { get; set; } /*"shpejt", "konsultim", "ekstra"*/
        public int? MaxBookingsPerDay { get; set; } /*Numri max i rezervimeve per sherbim"*/
        public string? AdditionalInfo { get; set; } /*Informata tjera shtese udhezime per sherbimin etj*/
        public decimal? DiscountPrice { get; set; } /*Cmimi me zbritje*/
        public string? Availability { get; set; } /*informata se kur o sherbimi aktivene oret e caktuara*/
        public string? RequiredMaterials { get; set; } /*informata se kur o sherbimi aktivene oret e caktuara*/
        public string ServiceCode { get; set; }  /*materiale te nevojshme*/
        public string? Currency { get; set; } /*materiale te nevojshme*/
        public string? ImageUrl { get; set; }  
        public int BusinessProfileId { get; set; }
        public BusinessProfile BusinessProfile { get; set; }
         

        public static Service Create(string name, string description, 
                                     decimal price, int durationInMinute,
                                     string? serviceType, string? tags,
                                     int? maxBookingsPerDay,string? additionalInfo,
                                     decimal? discountPrice,string? Availability,
                                     string? requiredMaterials, string serviceCode,
                                     string? currency,  string imageUrl,
                                     int bussinesProfileId)
        {
            var service = new Service
            {
                Name = name,
                Description = description,
                Price = price,
                DurationInMinutes = durationInMinute,
                ServiceType = serviceType,
                Tags = tags,
                MaxBookingsPerDay = maxBookingsPerDay,
                AdditionalInfo = additionalInfo,
                DiscountPrice = discountPrice,
                Availability = Availability,
                RequiredMaterials = requiredMaterials,
                ServiceCode = serviceCode,
                Currency = currency,
                ImageUrl = imageUrl, 
                BusinessProfileId = bussinesProfileId,
            };

            service.ValidateService();

            return service;
        }

        public void ChangeService(string name, string description,
                          decimal price, int durationInMinute,
                          string? serviceType, string? tags,
                          int? maxBookingsPerDay, string? additionalInfo,
                          decimal? discountPrice, string? availability,
                          string? requiredMaterials, string serviceCode,
                          string? currency, string imageUrl,
                          int businessProfileId)
        {
            Name = name;
            Description = description;
            Price = price;
            DurationInMinutes = durationInMinute;
            ServiceType = serviceType;
            Tags = tags;
            MaxBookingsPerDay = maxBookingsPerDay;
            AdditionalInfo = additionalInfo;
            DiscountPrice = discountPrice;
            Availability = availability;
            RequiredMaterials = requiredMaterials;
            ServiceCode = serviceCode;
            Currency = currency;
            ImageUrl = imageUrl;
            BusinessProfileId = businessProfileId;

            ValidateService();
        }
         

        public void ChangeName(string name)
        {
            Name = name;
            ValidateService();
        }

        public void ChangeDescription(string description)
        {
            Description = description;
            ValidateService();
        } 
        
        public void ChangePrice(decimal price)
        {
            Price = price;
            ValidateService();
        } 
        
        public void ChangeDurationInMinutes(int durationInMinutes)
        {
            DurationInMinutes = durationInMinutes;
            ValidateService();
        } 
        
        public void ChangeBusinessProfileId(int businessProfileId)
        {
            BusinessProfileId = businessProfileId;
            ValidateService();
        }

        public void ChangeToActive()
        {
            IsActive = true;
            ValidateService();
        }
        public void ChangeToDelete()
        {
            IsActive = false;
            ValidateService();
        }
        private void ValidateService()
        {
            if (string.IsNullOrWhiteSpace(Name))
                ThrowDomainException("Name is required.");

            if (string.IsNullOrWhiteSpace(Description))
                ThrowDomainException("Description is required."); 
            
            if (Price == null)
                ThrowDomainException("Price is required.");

            if (DurationInMinutes == null)
                ThrowDomainException("DurationInMinutes is required.");
             
        }
    }
}