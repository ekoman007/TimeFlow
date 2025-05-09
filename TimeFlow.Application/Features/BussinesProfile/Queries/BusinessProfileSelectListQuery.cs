using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.Application.Features.BussinesProfile.DTOs;
using TimeFlow.Application.Features.Category.DTOs;

namespace TimeFlow.Application.Features.BussinesProfile.Queries
{
    public class BusinessProfileSelectListQuery : IRequest<List<BussinesProfileSelectListModel>> { }
}

