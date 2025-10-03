using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using HR.LeaveManagement.Domain;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveTypes.Queries;

public class GetLeaveTypesQueryHandlerTests
{
    private readonly Mock<ILeaveTypeRepository> _leaveTypeRepositoryMock;
    private readonly Mock<IAppLogger<GetLeaveTypesQueryHandler>> _loggerMock;
    private readonly IMapper _mapper;
    
    public GetLeaveTypesQueryHandlerTests()
    {
        _leaveTypeRepositoryMock = MockLeaveTypeRepository.GetMockLeaveTypeRepository();
        _loggerMock = new Mock<IAppLogger<GetLeaveTypesQueryHandler>>();
        
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        }, new LoggerFactory());
        
        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task GetLeaveTypesTest()
    {
        var handler = new GetLeaveTypesQueryHandler(_mapper, _leaveTypeRepositoryMock.Object, _loggerMock.Object);
        var result = await handler.Handle(new GetLeaveTypesQuery(), CancellationToken.None);
        
        result.ShouldBeOfType<List<LeaveTypeDto>>();
        result.Count.ShouldBe(3);
    }
}