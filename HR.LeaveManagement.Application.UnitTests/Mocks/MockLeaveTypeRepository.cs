using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using Moq;

namespace HR.LeaveManagement.Application.UnitTests.Mocks;

public class MockLeaveTypeRepository
{
    public static Mock<ILeaveTypeRepository> GetMockLeaveTypeRepository()
    {
        var leaveTypes = new List<LeaveType>
        {
            new LeaveType
            {
                Id = 1,
                Name = "Test Leave Type 1",
                DefaultDays = 10,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            },
            new LeaveType
            {
                Id = 2,
                Name = "Test Leave Type 2",
                DefaultDays = 15,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            },
            new LeaveType
            {
                Id = 3,
                Name = "Test Leave Type 3",
                DefaultDays = 20,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            }
        };

        var mockRepo = new Mock<ILeaveTypeRepository>();

        mockRepo.Setup(r => r.GetAsync()).ReturnsAsync(leaveTypes);

        mockRepo.Setup(r => r.CreateAsync(It.IsAny<LeaveType>()))
            .ReturnsAsync((LeaveType leaveType) =>
            {
                leaveTypes.Add(leaveType);
                return leaveType.Id;
            });

        return mockRepo;
    }
}