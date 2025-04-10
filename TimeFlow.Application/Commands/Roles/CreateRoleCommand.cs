using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Commands.Roles
{
    public class CreateRoleCommand : IRequest<GeneralResponse<int>>
    {
        public string RoleName { get; set; }
        public string Description { get; set; } 
    }
}