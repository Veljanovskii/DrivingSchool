namespace DrivingSchool.Application.DataTransferObjects.SubmitTest;

public class SubmitTestResponse
{
    public int TotalScore { get; set; }
    public List<QuestionScoreDto> QuestionScores { get; set; } = new();
}