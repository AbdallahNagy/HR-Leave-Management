using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;

public class GetLeaveRequestDetailsQueryHandler(IMapper mapper, ILeaveRequestRepository leaveRequestRepository)
    : IRequestHandler<GetLeaveRequestDetailsQuery, LeaveRequestDetailsDto>
{
    public async Task<LeaveRequestDetailsDto> Handle(GetLeaveRequestDetailsQuery request, CancellationToken cancellationToken)
    {
        var leaveRequest = await leaveRequestRepository.GetLeaveRequestWithDetails(request.Id);
        return mapper.Map<LeaveRequestDetailsDto>(leaveRequest);
    }
}