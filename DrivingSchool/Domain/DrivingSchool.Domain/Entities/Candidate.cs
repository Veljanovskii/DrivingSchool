namespace DrivingSchool.Domain.Entities;

public record Candidate : User
{
    public List<TestResult> TestResults { get; init; } = new();

    private Candidate(UserId id, string username, string password)
        : base(id, username, password) { }

    public override bool CanManageTests() => false;

    public static Candidate Create(string username, string password)
    {
        return new Candidate(new UserId(Guid.NewGuid()), username, password);
    }
}
