namespace DrivingSchool.Application.DataTransferObjects.UpdateTest;

public class UpdateAnswerOptionRequest
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
}