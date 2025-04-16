 
using TimeFlow.SharedKernel;

namespace TimeFlow.Domain.Aggregates.UsersAggregates
{
    public class Category : AggregateRoot<int>
    {
        public string Name { get; set; }  
        public string Description { get; set; }  
        public string Code { get; set; }     
        public int IndustryId { get; set; }  
        public Industry Industry { get; set; }
         

        public static Category Create(string name, string description, string code, int industryId)
        {
            var category = new Category
            {
                Name = name,
                Description = description,
                Code = code,
                IndustryId = industryId
            };

            category.ValidateCategory();

            return category;
        }

        public void ChangeDescription(string description)
        {
            Description = description;
            ValidateCategory();
        }

        public void ChangeCode(string code)
        {
            Code = code;
            ValidateCategory();
        }
        public void ChangeIndustry(int industryId)
        {
            IndustryId = industryId;
            ValidateCategory();
        }

        public void ChangeName(string name)
        {
            Name = name;
            ValidateCategory();
        }

        public void Activate()
        {
            IsActive = true;
        }
        public void DeActivate()
        {
            IsActive = false;
        }

        private void ValidateCategory()
        {
            if (string.IsNullOrWhiteSpace(Name))
                ThrowDomainException("            if (string.IsNullOrWhiteSpace(Name))\r\n is required.");

            if (string.IsNullOrWhiteSpace(Description))
                ThrowDomainException("Description is required.");

            if (string.IsNullOrWhiteSpace(Code))
                ThrowDomainException("Code is required.");

        }
    }
}
