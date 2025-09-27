using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;

public class CancelLeaveRequestCommandHandler(
    ILeaveRequestRepository leaveRequestRepository, 
    IEmailSender emailSender,
    IAppLogger<CancelLeaveRequestCommandHandler> logger)
    : IRequestHandler<CancelLeaveRequestCommand, Unit>
{
    public async Task<Unit> Handle(CancelLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await leaveRequestRepository.GetByIdAsync(request.Id);

        if (leaveRequest is null)
        {
            throw new NotFoundException(nameof(LeaveRequest), request.Id);
        }

        leaveRequest.Canceled = true;
        await leaveRequestRepository.UpdateAsync(leaveRequest);

        // send email to requester
        try
        {
            var email = new EmailMessage
            {
                To = string.Empty,
                Body =
                    $"Your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} has been canceled.",
                Subject = "Leave Request Canceled"
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