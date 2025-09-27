using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestCommandHandler(
    IMapper mapper,
    IEmailSender emailSender,
    ILeaveRequestRepository leaveRequestRepository,
    ILeaveTypeRepository leaveTypeRepository,
    IAppLogger<UpdateLeaveRequestCommandHandler> logger)
    : IRequestHandler<UpdateLeaveRequestCommand, Unit>
{
    public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var validation = new UpdateLeaveRequestCommandValidator(leaveRequestRepository, leaveTypeRepository);
        var validationResult = await validation.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Count > 0)
            throw new BadRequestException("Invalid Leave Request", validationResult);

        var leaveRequest = await leaveRequestRepository.GetByIdAsync(request.Id);

        mapper.Map(request, leaveRequest);

        await leaveRequestRepository.UpdateAsync(leaveRequest!);

        try
        {
            var email = new EmailMessage
            {
                To = string.Empty,
                Body =
                    $"Your leave request for {leaveRequest!.StartDate:D} to {leaveRequest.EndDate:D} has been updated.",
                Subject = "Leave Request Updated"
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