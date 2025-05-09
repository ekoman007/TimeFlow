using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.Application.Features.Category.DTOs;
using TimeFlow.Application.Features.Services.DTOs;

namespace TimeFlow.Application.Features.Services.Queries
{
    public class ServiceSelectListQuery : IRequest<List<ServiceSelectListModel>> { }
     
}

