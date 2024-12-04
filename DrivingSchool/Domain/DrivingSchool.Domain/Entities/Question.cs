namespace DrivingSchool.Domain.Entities;

public record QuestionId(Guid Value);

public record Question
{
    public QuestionId Id { get; private init; }
    public string Text { get; private set; }
    public string? ImageUrl { get; private set; }
    public List<AnswerOption> AnswerOptions { get; private set; } = new();
    public int PointValue { get; private set; }

    private Question(QuestionId id, string text, string? imageUrl, int pointValue)
    {
        Id = id;
        Text = text;
        ImageUrl = imageUrl;
        PointValue = pointValue;
    }

    public static Question Create(string text, string? imageUrl, int pointValue)
    {
        if (pointValue < 1 || pointValue > 3)
        {
            throw new ArgumentOutOfRangeException(nameof(pointValue), "PointValue must be between 1 and 3.");
        }

        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException("Question text cannot be empty.");
        }

        return new Question(new QuestionId(Guid.NewGuid()), text, imageUrl, pointValue);
    }

    public void AddAnswerOption(AnswerOption answerOption)
    {
        if (AnswerOptions.Count >= 6)
        {
            throw new InvalidOperationException("Question cannot have more than 6 answer options.");
        }
        AnswerOptions.Add(answerOption);
    }

    public void UpdateTextAndImage(string text, string? imageUrl)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException("Question text cannot be empty.");
        }

        Text = text;
        ImageUrl = imageUrl;
    }
}
