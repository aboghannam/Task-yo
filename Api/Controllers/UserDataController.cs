using Application;
using Application.ClientData.Commands;
using Application.ClientData.Queries;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserDataController : ControllerBase
    {
        private IMediator _mediator;
        public UserDataController(IMediator mediator)
        {
             _mediator = mediator;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create([FromBody]CreateUserDataCommand request)
        {
            return new JsonResult(await _mediator.Send(request));
        }
        [HttpPut("Edti")]
        public async Task<ActionResult> Edit([FromBody]UpdateUserDataCommand request)
        {
            return new JsonResult(await _mediator.Send(request));
        }
        [HttpDelete("Delete")]
        public async Task<ActionResult> Delete([FromBody]DeleteUserDataCommand request)
        {
            return new JsonResult(await _mediator.Send(request));
        }
        [HttpGet("Find")]
        public async Task<ActionResult> Find([FromQuery] int Id)
        {
            return new JsonResult(await _mediator.Send(new GetUserQuery() { Id = Id }));
        }
        [HttpGet("GetAllData")]
        public async Task<ActionResult> List()
        {
            return new JsonResult(await _mediator.Send(new GetAllUsersQuery()));
        }
    }
}
