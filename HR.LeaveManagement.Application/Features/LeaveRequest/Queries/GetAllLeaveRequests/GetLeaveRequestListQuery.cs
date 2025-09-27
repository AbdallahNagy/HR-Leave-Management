using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetAllLeaveRequests;

public record GetLeaveRequestListQuery : IRequest<List<LeaveRequestListDto>>;