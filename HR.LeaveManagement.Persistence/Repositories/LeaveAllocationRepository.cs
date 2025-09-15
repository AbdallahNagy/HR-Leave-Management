using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories;

public class LeaveAllocationRepository(HrDatabaseContext context)
    : GenericRepository<LeaveAllocation>(context), ILeaveAllocationRepository
{
    public async Task<LeaveAllocation?> GetLeaveAllocationWithDetails(int id)
    {
        var leaveAllocation = await Context.LeaveAllocations
            .Include(la => la.LeaveType)
            .FirstOrDefaultAsync(la => la.Id == id);
        
        return leaveAllocation;
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
    {
        var leaveAllocations = await Context.LeaveAllocations
            .Include(la => la.LeaveType)
            .ToListAsync();
        
        return leaveAllocations;
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId)
    {
        var leaveAllocations = await Context.LeaveAllocations
            .Where(la => la.EmployeeId == userId)
            .Include(la => la.LeaveType)
            .ToListAsync();
        
        return leaveAllocations;
    }

    public async Task<bool> AllocationExists(string userId, int leaveTypeId, int period)
    {
        return await Context.LeaveAllocations 
            .AnyAsync(la => la.EmployeeId == userId
                           && la.LeaveTypeId == leaveTypeId
                           && la.Period == period);
    }

    public async Task AddAllocations(List<LeaveAllocation> allocations)
    {
        await Context.AddRangeAsync(allocations);
    }

    public async Task<List<LeaveAllocation>> GetUserAllocations(string userId, int leaveTypeId)
    {
        var userAllocations = await Context.LeaveAllocations
            .Where(la => la.EmployeeId == userId && la.LeaveTypeId == leaveTypeId)
            .Include(la => la.LeaveType)
            .ToListAsync();
        
        return userAllocations;
    }
}