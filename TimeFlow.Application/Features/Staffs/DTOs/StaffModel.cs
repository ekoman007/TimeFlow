using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeFlow.Application.Features.Staffs.DTOs
{
    public class StaffModel
    {
        public int Id { get; set; }
        public int BusinessProfileId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
    }
}
