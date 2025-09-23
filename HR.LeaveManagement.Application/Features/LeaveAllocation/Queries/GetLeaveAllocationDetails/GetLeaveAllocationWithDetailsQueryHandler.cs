using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;

public class GetLeaveAllocationWithDetailsQueryHandler(IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository) 
    : IRequestHandler<GetLeaveAllocationWithDetailsQuery, LeaveAllocationWithDetailsDto>
{
    public async Task<LeaveAllocationWithDetailsDto> Handle(GetLeaveAllocationWithDetailsQuery request, CancellationToken cancellationToken)
    {
        var leaveAllocation = await leaveAllocationRepository.GetLeaveAllocationWithDetails(request.Id);
        return mapper.Map<LeaveAllocationWithDetailsDto>(leaveAllocation);
    }
}
