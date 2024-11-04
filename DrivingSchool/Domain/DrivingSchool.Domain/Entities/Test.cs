namespace DrivingSchool.Domain.Entities;

public record TestId(Guid Value);

public record Test
{
    public TestId Id { get; private init; }
    public string Title { get; private init; }
    public int DurationInMinutes { get; private init; }
    public List<Question> Questions { get; private init; } = new();

    private Test(TestId id, string title, int durationInMinutes)
    {
        Id = id;
        Title = title;
        DurationInMinutes = durationInMinutes;
    }

    public static Test Create(string title, int durationInMinutes)
    {
        return new Test(new TestId(Guid.NewGuid()), title, durationInMinutes);
    }

    public void AddQuestion(Question question)
    {
        if (Questions.Count >= 6)
        {
            throw new InvalidOperationException("Test cannot have more than 6 questions.");
        }
        Questions.Add(question);
    }
}
