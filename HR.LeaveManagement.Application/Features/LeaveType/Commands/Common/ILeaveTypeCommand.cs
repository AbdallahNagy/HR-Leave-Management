namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.Common;

public interface ILeaveTypeCommand
{
    string Name { get; set; }
    int DefaultDays { get; set; }
}

