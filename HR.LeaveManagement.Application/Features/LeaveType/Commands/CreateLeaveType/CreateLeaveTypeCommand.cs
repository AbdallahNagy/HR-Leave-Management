using HR.LeaveManagement.Application.Features.LeaveType.Commands.Common;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommand : IRequest<int>, ILeaveTypeCommand
{
    public string Name { get; set; } = string.Empty;
    public int DefaultDays { get; set; }
}