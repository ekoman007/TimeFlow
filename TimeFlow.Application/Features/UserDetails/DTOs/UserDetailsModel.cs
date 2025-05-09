using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeFlow.Application.Features.UserDetails.DTOs
{
    public class UserDetailsModel
    {
        public int Id { get; set; }
        public string FullName { get; set; } 
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string ProfilePicture { get; set; }
        public int UserId { get; set; }
    }
}

