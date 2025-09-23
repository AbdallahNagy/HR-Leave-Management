using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations;

public class GetLeaveAllocationListQueryHandler(IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository) 
    : IRequestHandler<GetLeaveAllocationListQuery, List<LeaveAllocationDto>>
{
    public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationListQuery request, CancellationToken cancellationToken)
    {
        var leaveAllocations = await leaveAllocationRepository.GetLeaveAllocationsWithDetails();
        return mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);
    }
}