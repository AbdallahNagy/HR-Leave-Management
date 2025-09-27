using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveRequest.Shared;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommandValidator : AbstractValidator<CreateLeaveRequestCommand>
{
    public CreateLeaveRequestCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        Include(new BaseLeaveRequestValidator(leaveTypeRepository));
    }
}