using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.Common;

public class LeaveTypeCommandValidatorRules<T>(AbstractValidator<T> validator, ILeaveTypeRepository leaveTypeRepository)
    where T : ILeaveTypeCommand
{
    public void ApplyRules()
    {
        validator.RuleFor(x => x.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(70).WithMessage("{PropertyName} must not exceed 70 characters");

        validator.RuleFor(x => x.DefaultDays)
            .LessThan(101).WithMessage("{PropertyName} cannot exceed 100") 
            .GreaterThan(0).WithMessage("{PropertyName} cannot be less than 1");
        
        validator.RuleFor(x => x)
            .MustAsync(LeaveTypeUnique)
            .WithMessage("Leave type already exists");
    }
    
    private Task<bool> LeaveTypeUnique(T command, CancellationToken token)
    {
        return leaveTypeRepository.IsLeaveTypeUnique(command.Name);
    }
}