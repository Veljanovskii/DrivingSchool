namespace DrivingSchool.Application.DataTransferObjects.CreateTest;

public class CreateQuestionRequest
{
    public string Text { get; set; } = string.Empty;
    public List<string> Options { get; set; } = new();
    public List<int> CorrectOptionIndexes { get; set; } = new();
    public string? ImageUrl { get; set; }
}