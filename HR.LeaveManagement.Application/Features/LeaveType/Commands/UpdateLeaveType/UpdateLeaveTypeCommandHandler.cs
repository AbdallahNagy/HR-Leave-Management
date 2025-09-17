using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
    : IRequestHandler<UpdateLeaveTypeCommand, Unit>
{
    public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await new UpdateLeaveTypeCommandValidator(leaveTypeRepository)
            .ValidateAsync(request, cancellationToken);
        
        if (validationResult.Errors.Count > 0)
            throw new BadRequestException("Invalid Leave Type", validationResult);
        
        var leaveType = await leaveTypeRepository.GetByIdAsync(request.Id);

        if (leaveType == null) throw new NotFoundException(nameof(LeaveType), request.Id);

        var leaveTypeToUpdate = mapper.Map<Domain.LeaveType>(request);

        await leaveTypeRepository.UpdateAsync(leaveTypeToUpdate);
        return Unit.Value;
    }
}