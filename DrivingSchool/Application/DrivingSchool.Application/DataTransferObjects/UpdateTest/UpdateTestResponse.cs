namespace DrivingSchool.Application.DataTransferObjects.UpdateTest;

public class UpdateTestResponse
{
    public Guid TestId { get; set; }
    public string Title { get; set; } = string.Empty;
    public int DurationInMinutes { get; set; }
    public int TotalQuestions { get; set; }
}