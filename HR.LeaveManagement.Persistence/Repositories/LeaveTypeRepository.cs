using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories;

public class LeaveTypeRepository(HrDatabaseContext context)
    : GenericRepository<LeaveType>(context), ILeaveTypeRepository
{
    public async Task<bool> IsLeaveTypeUnique(string name)
    {
        return !await Context.LeaveTypes.AnyAsync(lt => lt.Name == name);
    }
}