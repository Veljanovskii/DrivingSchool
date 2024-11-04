using DrivingSchool.Domain.Entities;
using DrivingSchool.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Persistence.Context;

public class DrivingSchoolDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<Moderator> Moderators { get; set; }
    public DbSet<Test> Tests { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<AnswerOption> AnswerOptions { get; set; }
    public DbSet<TestResult> TestResults { get; set; }

    public DrivingSchoolDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new CandidateConfiguration());
        builder.ApplyConfiguration(new ModeratorConfiguration());
        builder.ApplyConfiguration(new TestConfiguration());
        builder.ApplyConfiguration(new QuestionConfiguration());
        builder.ApplyConfiguration(new AnswerOptionConfiguration());
        builder.ApplyConfiguration(new TestResultConfiguration());
    }
}