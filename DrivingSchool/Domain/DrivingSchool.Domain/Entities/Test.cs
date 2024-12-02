namespace DrivingSchool.Domain.Entities;

public record TestId(Guid Value);

public record Test
{
    public TestId Id { get; private init; }
    public string Title { get; private set; }
    public int DurationInMinutes { get; private set; }
    public List<Question> Questions { get; private init; } = new();

    private Test(TestId id, string title, int durationInMinutes)
    {
        Id = id;
        Title = title;
        DurationInMinutes = durationInMinutes;
    }

    public static Test Create(User user, string title, int durationInMinutes)
    {
        if (!user.CanManageTests())
        {
            throw new InvalidOperationException("Only moderators can create tests.");
        }

        if (title.Length is <= 0 or >= 100)
        {
            throw new ArgumentOutOfRangeException(nameof(title), "Title length must be between 1 and 99 characters.");
        }

        if (durationInMinutes is < 3 or >= 46)
        {
            throw new ArgumentOutOfRangeException(nameof(durationInMinutes), "Duration must be between 3 and 45 minutes.");
        }

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

    public void UpdateTitleAndDuration(User user, string title, int durationInMinutes)
    {
        if (!user.CanManageTests())
        {
            throw new InvalidOperationException("Only moderators can edit tests.");
        }

        if (title.Length is <= 0 or >= 100)
        {
            throw new ArgumentOutOfRangeException(nameof(title), "Title length must be between 1 and 99 characters.");
        }

        if (durationInMinutes is < 3 or >= 46)
        {
            throw new ArgumentOutOfRangeException(nameof(durationInMinutes), "Duration must be between 3 and 45 minutes.");
        }

        Title = title;
        DurationInMinutes = durationInMinutes;
    }
}
