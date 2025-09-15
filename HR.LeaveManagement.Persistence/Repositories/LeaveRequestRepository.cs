using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories;

public class LeaveRequestRepository(HrDatabaseContext context)
    : GenericRepository<LeaveRequest>(context), ILeaveRequestRepository
{
    public async Task<LeaveRequest?> GetLeaveRequestWithDetails(int id)
    {
        var leaveRequest = await Context.LeaveRequests
            .Include(lr => lr.LeaveType)
            .FirstOrDefaultAsync(lr => lr.Id == id);
        
        return leaveRequest;
    }

    public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails()
    {
        var leaveRequests = await Context.LeaveRequests
            .Include(lr => lr.LeaveType)
            .ToListAsync();
        
        return leaveRequests;
    }

    public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails(string userId)
    {
        var leaveRequests = await Context.LeaveRequests
            .Where(lr => lr.RequestingEmployeeId == userId)
            .Include(lr => lr.LeaveType)
            .ToListAsync();
        
        return leaveRequests;
    }
}