using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Persistence.Configurations;

public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
{
    public void Configure(EntityTypeBuilder<LeaveType> builder)
    {
        builder.HasData(
            new LeaveType
            {
                Id = 1,
                Name = "Vacation",
                DefaultDays = 10,
                DateCreated = new DateTime(2023, 3, 5),
                DateModified = new DateTime(2023, 12, 31)
            },
            new LeaveType
            {
                Id = 2,
                Name = "Sick",
                DefaultDays = 12,
                DateCreated = new DateTime(2025, 09, 05),
                DateModified = new DateTime(2025, 12, 31)
            }
        );

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}