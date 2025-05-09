using MediatR; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeFlow.Application.DTOs;
using TimeFlow.Application.Features.Appointments.Commands;
using TimeFlow.Application.Features.Appointments.Queries;
using TimeFlow.Application.Responses;

namespace TimeFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : DefaultController
    {
        public AppointmentsController(IMediator mediator)
           : base(mediator)
        {
            //
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<GeneralResponse<int>>> CreateAppointment([FromBody] CreateAppointmentCommand command)
        {
            return await Mediator.Send(command).ConfigureAwait(false);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<GeneralResponse<AppointmentDto>>> GetAppointmentById(int id)
        {
            return await Mediator.Send(new GetAppointmentByIdQuery { Id = id }).ConfigureAwait(false);
        }

        [HttpGet("user")]
        [Authorize]
        public async Task<ActionResult<GeneralResponse<List<AppointmentDto>>>> GetUserAppointments([FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null)
        {
            var userId = GetUserIdFromClaims();
            return await Mediator.Send(new GetUserAppointmentsQuery(userId, startDate, endDate)).ConfigureAwait(false);
        }

        [HttpGet("staff/{staffId}")]
        [Authorize]
        public async Task<ActionResult<GeneralResponse<List<AppointmentDto>>>> GetStaffAppointments(int staffId, [FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null)
        {
            return await Mediator.Send(new GetStaffAppointmentsQuery(staffId, startDate, endDate)).ConfigureAwait(false);
        }

        [HttpGet("business/{businessId}")]
        [Authorize]
        public async Task<ActionResult<GeneralResponse<List<AppointmentDto>>>> GetBusinessAppointments(int businessId, [FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null)
        {
            return await Mediator.Send(new GetBusinessAppointmentsQuery(businessId, startDate, endDate)).ConfigureAwait(false);
        }

        [HttpGet("available-slots")]
        public async Task<ActionResult<GeneralResponse<List<TimeSlotDto>>>> GetAvailableTimeSlots([FromQuery] int serviceId, [FromQuery] int staffId, [FromQuery] DateTime date)
        {
            return await Mediator.Send(new GetAvailableTimeSlotsQuery(serviceId, staffId, date)).ConfigureAwait(false);
        }

        [HttpPut("{id}/cancel")]
        [Authorize]
        public async Task<ActionResult<GeneralResponse<int>>> CancelAppointment(int id)
        {
            return await Mediator.Send(new CancelAppointmentCommand(id)).ConfigureAwait(false);
        }

        [HttpPut("{id}/complete")]
        [Authorize]
        public async Task<ActionResult<GeneralResponse<AppointmentDto>>> CompleteAppointment(int id)
        {
            return await Mediator.Send(new CompleteAppointmentCommand(id)).ConfigureAwait(false);
        }

        [HttpPut("{id}/reschedule")]
        [Authorize]
        public async Task<ActionResult<GeneralResponse<AppointmentDto>>> RescheduleAppointment(int id, [FromBody] RescheduleAppointmentCommand command)
        {
            command.Id = id;
            return await Mediator.Send(command).ConfigureAwait(false);
        }

        private int GetUserIdFromClaims()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "sub" || c.Type == "userId");
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
            {
                throw new UnauthorizedAccessException("Invalid user information");
            }
            return userId;
        }
    }
}