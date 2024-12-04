namespace DrivingSchool.Application.DataTransferObjects.SubmitTest;

public class QuestionAnswerDto
{
    public Guid QuestionId { get; set; }
    public List<int> SelectedOptionIds { get; set; } = new();
}