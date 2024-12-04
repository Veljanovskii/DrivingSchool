namespace DrivingSchool.Application.DataTransferObjects.CreateTest;

public class CreateQuestionRequest
{
    public string Text { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public List<CreateAnswerOptionRequest> Options { get; set; } = new();
    public int PointValue { get; set; }
}