using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandValidator : AbstractValidator<UpdateLeaveAllocationCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;

    public UpdateLeaveAllocationCommandValidator(ILeaveTypeRepository leaveTypeRepository, ILeaveAllocationRepository leaveAllocationRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _leaveAllocationRepository = leaveAllocationRepository;

        RuleFor(p => p.NumberOfDays)
            .GreaterThan(0)
            .WithMessage("{PropertyName} must be greater than {ComparisonValue}.");
        
        RuleFor(p => p.Period)
            .GreaterThanOrEqualTo(DateTime.Now.Year)
            .WithMessage("{PropertyName} must be after {ComparisonValue}.");
        
        RuleFor(v => v.LeaveTypeId)
            .GreaterThan(0)
            .MustAsync(LeaveTypeExist)
            .WithMessage("{PropertyName} does not exist}");
        
        RuleFor(v => v.Id)
            .GreaterThan(0)
            .MustAsync(LeaveAllocationExist)
            .WithMessage("{PropertyName} does not exist}");
    }
    
    private async Task<bool> LeaveAllocationExist(int id, CancellationToken token)
    {
        var leaveAllocation = await _leaveAllocationRepository.GetByIdAsync(id);
        return leaveAllocation != null;
    }
    
    private async Task<bool> LeaveTypeExist(int id, CancellationToken token)
    {
        var leaveType = await _leaveTypeRepository.GetByIdAsync(id);
        return leaveType != null;
    }
}