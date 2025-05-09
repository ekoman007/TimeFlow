using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.Industry.Commands
{
    public class UpdateIndustryCommand : IRequest<GeneralResponse<int>>
    {
        public int Id{ get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
    }
}

