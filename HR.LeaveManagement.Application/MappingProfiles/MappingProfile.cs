using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetAllLeaveRequests;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.MappingProfiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<LeaveType, LeaveTypeDto>();
        CreateMap<LeaveType, LeaveTypeDetailsDto>();
        CreateMap<CreateLeaveTypeCommand, LeaveType>();
        CreateMap<UpdateLeaveTypeCommand, LeaveType>();
        CreateMap<LeaveAllocation, LeaveAllocationDto>();
        CreateMap<LeaveAllocation, LeaveAllocationWithDetailsDto>();
        CreateMap<CreateLeaveAllocationCommand, LeaveAllocation>();
        CreateMap<LeaveRequest, LeaveRequestListDto>();
        CreateMap<CreateLeaveRequestCommand, LeaveRequest>();
        CreateMap<UpdateLeaveRequestCommand, LeaveRequest>();
    }
}