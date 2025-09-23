using HR.LeaveManagement.Api.Filters;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveTypesController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [ServiceFilter(typeof(ResourceFilter))]
        public async Task<List<LeaveTypeDto>> Get()
        {
            var leaveTypes = await mediator.Send(new GetLeaveTypesQuery());
            return leaveTypes;
        }

        [HttpGet("{id}")]
        [ServiceFilter(typeof(ResourceFilter))]
        public async Task<ActionResult<LeaveTypeDetailsDto>> Get(int id)
        {
            var leaveType = await mediator.Send(new GetLeaveTypeDetailsQuery(id));
            return Ok(leaveType);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ServiceFilter(typeof(AuthorizedUser))]
        public async Task<ActionResult> Post([FromBody] CreateLeaveTypeCommand createLeaveTypeCommand)
        {
            var response = await mediator.Send(createLeaveTypeCommand);
            return CreatedAtAction(nameof(Get), new { id = response });
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Put([FromBody] UpdateLeaveTypeCommand updateLeaveTypeCommand)
        {
            await mediator.Send(updateLeaveTypeCommand);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await mediator.Send(new DeleteLeaveTypeCommand(id));
        }
    }
}
