using TimeFlow.SharedKernel;

namespace TimeFlow.Domain.Aggregates.UsersAggregates.Roles
{
    public class Role : AggregateRoot<int>
    {
        public string RoleName { get; set; }
        public string Description { get; set; }
        public ICollection<User> Users { get; private set; } = new List<User>();
         
        public static Role Create(string rolename, string description)
        {
            var role = new Role
            {
                RoleName = rolename,
                Description = description
            };

            role.ValidateRole();

            return role;
        }

        public void ChangeRoleName(string rolename)
        {
            RoleName = rolename;
            ValidateRole();
        }

        public void ChangeDescription(string description)
        {
            Description = description;
            ValidateRole();
        }
          
        public void ChangeStatusToActive()
        {
            Status = EntityStatus.Active;   
            ValidateRole();
        }
        public void ChangeStatusToDelete()
        {
            Status = EntityStatus.Deleted;   
            ValidateRole();
        }
        private void ValidateRole()
        {
            if (string.IsNullOrWhiteSpace(RoleName))
                ThrowDomainException("RoleName is required.");

            if (string.IsNullOrWhiteSpace(Description))
                ThrowDomainException("Description is required."); 
        }
    } 
}
