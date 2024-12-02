namespace DrivingSchool.Application.DataTransferObjects.UpdateTest;

public class UpdateTestRequest
{
    public Guid TestId { get; set; }
    public string Title { get; set; } = string.Empty;
    public int DurationInMinutes { get; set; }
    public List<UpdateQuestionRequest> Questions { get; set; } = new();
    public Guid UserId { get; set; }
}