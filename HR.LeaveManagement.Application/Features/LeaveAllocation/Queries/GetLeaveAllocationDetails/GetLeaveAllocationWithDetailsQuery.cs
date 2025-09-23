using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;

public record GetLeaveAllocationWithDetailsQuery(int Id) : IRequest<LeaveAllocationWithDetailsDto>;