namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;

public class LeaveRequestDetailsDto
{
    public string RequestingEmployeeId { get; set; } = string.Empty;
    public Domain.LeaveType LeaveType { get; set; } = new Domain.LeaveType();
    public int LeaveTypeId { get; set; }
    public DateTime DateRequested { get; set; }
    public string RequestComments { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime DateActioned { get; set; }
    public bool? Approved { get; set; }
    public bool Cancelled { get; set; }
}