using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.SharedKernel;

namespace TimeFlow.Domain.Aggregates.UsersAggregates
{
    public class UserDetails : BaseEntity
    {

        public string FullName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string ProfilePicture { get; set; }
        public int UserId { get; set; }
        public ApplicationUser User { get; set; } 
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }

}
