namespace DrivingSchool.Application.DataTransferObjects.CreateTest;

public class CreateAnswerOptionRequest
{
    public string Text { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
}