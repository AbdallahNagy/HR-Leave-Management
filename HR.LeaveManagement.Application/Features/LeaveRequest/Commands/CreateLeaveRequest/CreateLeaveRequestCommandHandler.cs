using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommandHandler(
    IMapper mapper,
    IEmailSender emailSender,
    ILeaveRequestRepository leaveRequestRepository,
    ILeaveTypeRepository leaveTypeRepository,
    IAppLogger<CreateLeaveRequestCommandHandler> logger)
    : IRequestHandler<CreateLeaveRequestCommand, int>
{
    public async Task<int> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var validation = new CreateLeaveRequestCommandValidator(leaveTypeRepository);
        var validationResult = await validation.ValidateAsync(request, cancellationToken);
        
        if (validationResult.Errors.Count != 0)
            throw new BadRequestException("Invalid Leave Request", validationResult);
        
        var leaveRequest = mapper.Map<Domain.LeaveRequest>(request);
        var id = await leaveRequestRepository.CreateAsync(leaveRequest);
        
        // Send Email Notification
        try
        {
            var email = new EmailMessage
            {
                To = "admin@localhost",
                Body = "A new leave request has been submitted",
                Subject = "New Leave Request Submitted"
            };
            await emailSender.SendEmail(email);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex.Message);
        }
        
        return id;
    }
}