namespace DrivingSchool.Application.DataTransferObjects.SubmitTest;

public class SubmitTestRequest
{
    public Guid TestResultId { get; set; }
    public List<QuestionAnswerDto> Answers { get; set; } = new();
}