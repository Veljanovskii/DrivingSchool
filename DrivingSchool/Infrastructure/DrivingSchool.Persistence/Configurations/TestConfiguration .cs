using DrivingSchool.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Persistence.Configurations;

public class TestConfiguration : IEntityTypeConfiguration<Test>
{
    public void Configure(EntityTypeBuilder<Test> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => new TestId(value)
            );
        builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
        builder.Property(x => x.DurationInMinutes).IsRequired();
        builder.HasMany(t => t.Questions).WithOne().OnDelete(DeleteBehavior.Cascade);
    }
}
