namespace DrivingSchool.Domain.Entities;

public record TestResult
{
    public TestId TestId { get; private set; }
    public UserId CandidateId { get; private set; }
    public int Score { get; private set; }
    public DateTime TakenAt { get; private set; }

    private TestResult() { }

    private TestResult(TestId testId, UserId candidateId, int score, DateTime takenAt)
    {
        TestId = testId;
        CandidateId = candidateId;
        Score = score;
        TakenAt = takenAt;
    }

    public static TestResult Create(TestId testId, UserId candidateId, int score)
    {
        return new TestResult(testId, candidateId, score, DateTime.UtcNow);
    }

    public TestResult UpdateScore(int newScore)
    {
        if (newScore < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(newScore), "Score cannot be negative.");
        }

        Score = newScore;
        return this;
    }
}
