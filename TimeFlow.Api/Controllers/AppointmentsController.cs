using MediatR; 
using Microsoft.AspNetCore.Mvc;
using TimeFlow.Application.Features.Appointments.Commands; 
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

        [HttpPost("create")]
        public async Task<GeneralResponse<int>> AppointmentCreate([FromBody] CreateAppointmentCommand command)
        {
            return await Mediator.Send(command).ConfigureAwait(false);
        }
    }
}