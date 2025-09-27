using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetAllLeaveRequests;

public class GetLeaveRequestListQueryHandler(IMapper mapper, ILeaveRequestRepository leaveRequestRepository)
    : IRequestHandler<GetLeaveRequestListQuery, List<LeaveRequestListDto>>
{
    public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestListQuery request, CancellationToken cancellationToken)
    {
        var leaveRequests = await leaveRequestRepository.GetLeaveRequestsWithDetails();
        return mapper.Map<List<LeaveRequestListDto>>(leaveRequests);
    }
}