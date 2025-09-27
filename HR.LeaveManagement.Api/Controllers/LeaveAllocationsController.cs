using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveAllocationsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<LeaveAllocationDto>>> Get(bool isLoggedInUser = false)
    {
        var leaveAllocations = await mediator.Send(new GetLeaveAllocationListQuery());
        return Ok(leaveAllocations);
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<LeaveAllocationWithDetailsDto>> Get(int id)
    {
        var leaveAllocation = await mediator.Send(new GetLeaveAllocationWithDetailsQuery(id));
        return Ok(leaveAllocation);
    }

    [HttpPost]
    public async Task<ActionResult> Post(CreateLeaveAllocationCommand leaveAllocation)
    {
        var response = await mediator.Send(leaveAllocation);
        return CreatedAtAction(nameof(Get), new { id = response });
    }

    [HttpPut]
    public async Task<ActionResult> Put(UpdateLeaveAllocationCommand leaveAllocation)
    {
        await mediator.Send(leaveAllocation);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        await mediator.Send(new DeleteLeaveAllocationCommand{ Id = id });
        return NoContent();
    }
}