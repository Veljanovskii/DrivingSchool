namespace DrivingSchool.Domain.Entities;

public record AnswerOption
{
    public int Id { get; private init; }
    public string Text { get; private set; }
    public bool IsCorrect { get; private set; }

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

    public void Update(string text, bool isCorrect)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException("Answer option text cannot be empty.");
        }

        Text = text;
        IsCorrect = isCorrect;
    }
}
