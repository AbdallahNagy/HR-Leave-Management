using HR.LeaveManagement.Application.Features.LeaveType.Commands.Common;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommand : IRequest<Unit>, ILeaveTypeCommand
{
    public string Name { get; set; } = string.Empty;
    public int DefaultDays { get; set; }
}