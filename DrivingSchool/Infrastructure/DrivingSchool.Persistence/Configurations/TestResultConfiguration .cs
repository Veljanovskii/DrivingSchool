using DrivingSchool.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Persistence.Configurations;

public class TestResultConfiguration : IEntityTypeConfiguration<TestResult>
{
    public void Configure(EntityTypeBuilder<TestResult> builder)
    {
        builder.HasKey(x => x.TestId);
        builder.Property(x => x.TestId)
            .HasConversion(
                id => id.Value,
                value => new TestId(value)
            );
        builder.Property(x => x.Score).IsRequired();
        builder.Property(x => x.TakenAt).IsRequired();
    }
}
