namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetAllLeaveRequests;

public class LeaveRequestListDto
{
    public string RequestingEmployeeId { get; set; } = string.Empty;
    public Domain.LeaveType LeaveType { get; set; } = new Domain.LeaveType();
    public DateTime DateRequested { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool? Approved { get; set; }
}