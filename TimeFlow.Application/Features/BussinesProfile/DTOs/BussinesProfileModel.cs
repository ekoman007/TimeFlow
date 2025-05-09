using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeFlow.Application.Features.BussinesProfile.DTOs
{
    public class BussinesProfileModel
    {
        public int Id { get; set; }
        public string BusinessName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Website { get; set; }
        public string Description { get; set; }
        public string LogoUrl { get; set; }
        public int IndustryId { get; set; } 
        public int UserDetailsId { get; set; }
    }
}

