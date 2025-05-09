using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.Staffs.Commands
{
    public class DeleteStaffCommand : IRequest<GeneralResponse<int>>
    {
        public int Id { get; set; }
    }
}
