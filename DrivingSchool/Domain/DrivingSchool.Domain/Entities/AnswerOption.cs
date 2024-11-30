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

    public static AnswerOption Create(string text, bool isCorrect)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException("Answer option text cannot be empty.");
        }

        return new AnswerOption(0, text, isCorrect);
    }
}
