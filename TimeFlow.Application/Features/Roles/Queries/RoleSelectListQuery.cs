﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.Application.Features.Roles.DTOs;

namespace TimeFlow.Application.Queries.Roles
{
    namespace TimeFlow.Application.Queries.Roles
    {
        public class RoleSelectListQuery : IRequest<List<RoleSelectListModel>> { }
    }
}
