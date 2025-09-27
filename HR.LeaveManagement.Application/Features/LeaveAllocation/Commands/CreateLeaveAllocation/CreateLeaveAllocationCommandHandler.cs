using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandHandler(
    ILeaveAllocationRepository leaveAllocationRepository, 
    ILeaveTypeRepository leaveTypeRepository,
    IMapper mapper) : IRequestHandler<CreateLeaveAllocationCommand, int>
{
    public async Task<int> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveAllocationCommandValidator(leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (validationResult.Errors.Count > 0)
        {
            throw new BadRequestException("Invalid Leave Allocation", validationResult);
        }
        
        var leaveType = await leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);
        
        var leaveAllocation = mapper.Map<Domain.LeaveAllocation>(request);
        var id = await leaveAllocationRepository.CreateAsync(leaveAllocation);

        return id;
    }
}