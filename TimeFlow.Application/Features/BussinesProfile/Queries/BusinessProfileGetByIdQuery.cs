using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeFlow.Application.Features.BussinesProfile.DTOs;
using TimeFlow.Application.Features.Category.DTOs;
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.BussinesProfile.Queries
{
    public class BusinessProfileGetByIdQuery : IRequest<GeneralResponse<BussinesProfileModel>>
    {
        public int Id { get; set; }
        public BusinessProfileGetByIdQuery(int id)
        {
            Id = id;
        }
    }
}