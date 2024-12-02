namespace DrivingSchool.Application.DataTransferObjects.UpdateTest;

public class UpdateQuestionRequest
{
    public Guid Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public List<UpdateAnswerOptionRequest> Options { get; set; } = new();
}