using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository, ILeaveAllocationRepository leaveAllocationRepository)
    : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
{
    public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validation = new UpdateLeaveAllocationCommandValidator(leaveTypeRepository, leaveAllocationRepository);
        var validationResult = await validation.ValidateAsync(request, cancellationToken);
        
        if (validationResult.Errors.Any())
            throw new BadRequestException("Invalid Leave Allocation", validationResult);
        
        var leaveAllocation = await leaveAllocationRepository.GetByIdAsync(request.Id);
        
        if(leaveAllocation == null)
            throw new NotFoundException(nameof(leaveAllocation), request.Id);
        
        mapper.Map(request, leaveAllocation);
        
        await leaveAllocationRepository.UpdateAsync(leaveAllocation);
        return Unit.Value;
    }
}