namespace DrivingSchool.Application.DataTransferObjects.CreateTest;

public class CreateTestRequest
{
    public string Title { get; set; } = string.Empty;
    public int DurationInMinutes { get; set; }
    public List<CreateQuestionRequest> Questions { get; set; } = new();
    public Guid UserId { get; set; }
}