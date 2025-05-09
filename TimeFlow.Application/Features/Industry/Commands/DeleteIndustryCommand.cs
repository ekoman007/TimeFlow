using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.Industry.Commands
{
    public class DeleteIndustryCommand : IRequest<GeneralResponse<int>>
    {
        public int Id { get; set; }
    }
}

