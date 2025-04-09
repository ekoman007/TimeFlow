using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TimeFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class DefaultController : ControllerBase
    {
        protected readonly IMediator Mediator;

        public DefaultController(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}
