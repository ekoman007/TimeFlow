 
using TimeFlow.SharedKernel;

namespace TimeFlow.Domain.Aggregates.UsersAggregates
{
    public class Industry : AggregateRoot<int>
    {
        public string Name { get; set; }  
        public string Description { get; set; }  
        public string Code { get; set; }
        public virtual ICollection<BusinessProfile> BusinessProfiles { get; set; } = new List<BusinessProfile>();
        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
        public static Industry Create(string name, string description, string code)
        {
            var industryEntity = new Industry
            {
                Name = name,
                Description = description,
                Code = code 
            };

            industryEntity.Validateindustry();

            return industryEntity;
        }

        public void ChangeDescription(string description)
        {
            Description = description;
            Validateindustry();
        }

        public void ChangeCode(string code)
        {
            Code = code;
            Validateindustry();
        }
 

        public void ChangeName(string name)
        {
            Name = name;
            Validateindustry();
        }

        public void Activate()
        {
            IsActive = true;
        }
        public void DeActivate()
        {
            IsActive = false;
        } 
        private void Validateindustry()
        {
            if (string.IsNullOrWhiteSpace(Name))
                ThrowDomainException("Name is required.");

            if (string.IsNullOrWhiteSpace(Description))
                ThrowDomainException("Description is required.");

            if (string.IsNullOrWhiteSpace(Code))
                ThrowDomainException("Code is required.");
             
        }
    }
}
