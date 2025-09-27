using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;

public class DeleteLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository) 
    : IRequestHandler<DeleteLeaveRequestCommand, Unit>
{
    public async Task<Unit> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await leaveRequestRepository.GetByIdAsync(request.Id);
        
        if (leaveRequest == null)
            throw new NotFoundException(nameof(LeaveRequest), request.Id);

        await leaveRequestRepository.DeleteAsync(leaveRequest);
        return Unit.Value;
    }
}