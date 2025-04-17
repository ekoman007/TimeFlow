using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.Staffs.Commands
{
    public class UpdateStaffCommand : IRequest<GeneralResponse<int>>
    {
        public int Id { get; set; }
        public int BusinessProfileId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
    }
}
