using MediatR; 
using TimeFlow.Application.Features.Staffs.DTOs;
using TimeFlow.Application.Responses;

namespace TimeFlow.Application.Features.Staffs.Queries
{
    public class StaffByIdQuery : IRequest<GeneralResponse<StaffModel>>
    {
        public int Id { get; set; }
        public StaffByIdQuery(int id)
        {
            Id = id;
        }
    }
}