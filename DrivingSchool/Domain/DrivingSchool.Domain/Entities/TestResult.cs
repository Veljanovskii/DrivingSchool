namespace DrivingSchool.Domain.Entities;

public record TestResult
{
    public TestId TestId { get; private init; }
    public int Score { get; private init; }
    public DateTime TakenAt { get; private init; }

    private TestResult(TestId testId, int score, DateTime takenAt)
    {
        TestId = testId;
        Score = score;
        TakenAt = takenAt;
    }

    public static TestResult Create(TestId testId, int score)
    {
        return new TestResult(testId, score, DateTime.UtcNow);
    }
}
