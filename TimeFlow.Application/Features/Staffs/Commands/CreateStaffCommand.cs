using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.Application.Responses;
using TimeFlow.Domain.Aggregates.UsersAggregates;

namespace TimeFlow.Application.Features.Staffs.Commands
{
    public class CreateStaffCommand : IRequest<GeneralResponse<int>>
    {
        public int BusinessProfileId { get; set; } 
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
    }
}