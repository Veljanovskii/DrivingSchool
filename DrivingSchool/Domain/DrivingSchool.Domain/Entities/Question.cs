namespace DrivingSchool.Domain.Entities;

public record QuestionId(Guid Value);

public record Question
{
    public QuestionId Id { get; private init; }
    public string Text { get; private init; }
    public string? ImageUrl { get; private init; }
    public List<AnswerOption> AnswerOptions { get; private init; } = new();

    private Question(QuestionId id, string text, string? imageUrl)
    {
        Id = id;
        Text = text;
        ImageUrl = imageUrl;
    }

    public static Question Create(string text, string? imageUrl = null)
    {
        return new Question(new QuestionId(Guid.NewGuid()), text, imageUrl);
    }

    public void AddAnswerOption(AnswerOption answerOption)
    {
        if (AnswerOptions.Count >= 6)
        {
            throw new InvalidOperationException("Question cannot have more than 6 answer options.");
        }
        AnswerOptions.Add(answerOption);
    }
}
