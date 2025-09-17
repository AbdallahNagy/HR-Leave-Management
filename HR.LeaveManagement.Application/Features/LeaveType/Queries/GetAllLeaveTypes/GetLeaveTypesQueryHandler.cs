using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

public class GetLeaveTypesQueryHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository, IAppLogger<GetLeaveTypesQueryHandler> logger)
    : IRequestHandler<GetLeaveTypesQuery, List<LeaveTypeDto>>
{
    public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypesQuery request, CancellationToken cancellationToken)
    {
        var leaveTypes  = await leaveTypeRepository.GetAsync(); 
        
        logger.LogInformation("Retrieved {Count} leave types", leaveTypes.Count);
        
        return mapper.Map<List<LeaveTypeDto>>(leaveTypes); 
    }
}