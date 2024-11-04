using DrivingSchool.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Persistence.Configurations;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => new QuestionId(value)
            );
        builder.Property(x => x.Text).IsRequired().HasMaxLength(300);
        builder.Property(x => x.ImageUrl).HasMaxLength(200);
        builder.HasMany(q => q.AnswerOptions).WithOne().OnDelete(DeleteBehavior.Cascade);
    }
}
