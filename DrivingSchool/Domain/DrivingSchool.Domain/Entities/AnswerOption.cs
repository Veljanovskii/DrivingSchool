namespace DrivingSchool.Domain.Entities;

public record AnswerOption
{
    public int Id { get; private init; }
    public string Text { get; private init; }
    public bool IsCorrect { get; private init; }

    public AnswerOption(int id, string text, bool isCorrect)
    {
        Id = id;
        Text = text;
        IsCorrect = isCorrect;
    }
}
