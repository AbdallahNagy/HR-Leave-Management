using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetAllLeaveRequests;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestsController(IMediator mediator) : ControllerBase
    {
        // GET: api/<LeaveRequestsController>
        [HttpGet]
        public async Task<ActionResult<List<LeaveRequestListDto>>> Get()
        {
            var leaveRequests = await mediator.Send(new GetLeaveRequestListQuery());
            return Ok(leaveRequests);
        }

        // GET api/<LeaveRequestsController>/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<LeaveRequestDetailsDto>> Get(int id)
        {
            var leaveRequest = await mediator.Send(new GetLeaveRequestDetailsQuery(id));
            return Ok(leaveRequest);
        }

        // POST api/<LeaveRequestsController>
        [HttpPost]
        public async Task<ActionResult> Post(CreateLeaveRequestCommand leaveRequestCommand)
        {
            var response = await mediator.Send(leaveRequestCommand);
            return CreatedAtAction(nameof(Get), new { id = response });
        }

        // PUT api/<LeaveRequestsController>/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(UpdateLeaveTypeCommand leaveTypeCommand)
        {
            await mediator.Send(leaveTypeCommand);
            return NoContent();
        }
        
        [HttpPut("CancelRequest")]
        public async Task<ActionResult> CancelRequest(CancelLeaveRequestCommand cancelLeaveRequestCommand)
        {
            await mediator.Send(cancelLeaveRequestCommand);
            return NoContent();
        }
        
        [HttpPut("UpdateApproval")]
        public async Task<ActionResult> UpdateApproval(ChangeLeaveRequestApprovalCommand changeLeaveRequestApprovalCommand)
        {
            await mediator.Send(changeLeaveRequestApprovalCommand);
            return NoContent();
        }

        // DELETE api/<LeaveRequestsController>/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await mediator.Send(new DeleteLeaveTypeCommand(id));
            return NoContent();
        }
    }
}
