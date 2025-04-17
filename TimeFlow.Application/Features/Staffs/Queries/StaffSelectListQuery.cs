using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.Application.Features.Services.DTOs;
using TimeFlow.Application.Features.Staffs.DTOs;

namespace TimeFlow.Application.Features.Staffs.Queries
{
    public class StaffSelectListQuery : IRequest<List<StaffSelectListModel>> { }
     
}
