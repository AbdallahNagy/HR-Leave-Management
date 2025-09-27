using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;

public class ChangeLeaveRequestApprovalCommandHandler(
    ILeaveRequestRepository leaveRequestRepository,
    IEmailSender emailSender,
    IAppLogger<ChangeLeaveRequestApprovalCommandHandler> logger) : IRequestHandler<ChangeLeaveRequestApprovalCommand, Unit>
{
    public async Task<Unit> Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await leaveRequestRepository.GetByIdAsync(request.Id);
        
        if (leaveRequest == null)
            throw new NotFoundException(nameof(LeaveRequest), request.Id);
        
        leaveRequest.Approved = request.Approved;
        await leaveRequestRepository.UpdateAsync(leaveRequest);
        
        // Send Email Notification
        try
        {
            var email = new EmailMessage
            {
                To = "user@localhost",
                Body =
                    $"Your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} has been {(request.Approved ? "approved" : "rejected")}.",
                Subject = "Leave Request Approval Status Changed"
            };

            await emailSender.SendEmail(email);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex.Message);
        }
        
        return Unit.Value;
    }
}