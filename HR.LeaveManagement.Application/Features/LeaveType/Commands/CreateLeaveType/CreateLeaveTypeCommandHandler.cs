using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandHandler(
    IMapper mapper,
    ILeaveTypeRepository leaveTypeRepository,
    IAppLogger<CreateLeaveTypeCommandHandler> logger)
    : IRequestHandler<CreateLeaveTypeCommand, Unit>
{
    public async Task<Unit> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await new CreateLeaveTypeCommandValidator(leaveTypeRepository)
            .ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Count > 0)
        {
            logger.LogWarning("Validation errors in create leave type request {0}", validationResult);
            throw new BadRequestException("Invalid Leave Type", validationResult);
        }

        var leaveType = mapper.Map<Domain.LeaveType>(request);

        await leaveTypeRepository.CreateAsync(leaveType);

        logger.LogInformation($"Leave type {leaveType.Name} created successfully");

        return Unit.Value;
    }
}