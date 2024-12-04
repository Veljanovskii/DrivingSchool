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
            )
            .IsRequired();

        builder.Property(x => x.CandidateId)
            .HasConversion(
                id => id.Value,
                value => new UserId(value)
            )
            .IsRequired();

        builder.Property(x => x.Score).IsRequired();
        builder.Property(x => x.TakenAt).IsRequired();

        builder.HasOne<Candidate>()
            .WithMany(c => c.TestResults)
            .HasForeignKey(x => x.CandidateId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
