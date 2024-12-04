using System.Net;
using DrivingSchool.Application.DataTransferObjects.SubmitTest;
using DrivingSchool.Application.Exceptions;
using DrivingSchool.Domain.Contracts;
using DrivingSchool.Domain.Entities;
using MediatR;

namespace DrivingSchool.Application.Features.SubmitTest;

public record SubmitTestCommand(SubmitTestRequest Request) : IRequest<SubmitTestResponse>;

public class SubmitTestCommandHandler(ITestRepository testRepository, 
    ITestResultRepository testResultRepository)
    : IRequestHandler<SubmitTestCommand, SubmitTestResponse>
{
    private readonly ITestRepository _testRepository = testRepository;
    private readonly ITestResultRepository _testResultRepository = testResultRepository;

    public async Task<SubmitTestResponse> Handle(SubmitTestCommand request, CancellationToken cancellationToken)
    {
        var testResult = await _testResultRepository.ReadAsync(new TestId(request.Request.TestResultId));
        if (testResult == null)
        {
            throw new HttpException(HttpStatusCode.NotFound, "Test result not found.");
        }

        var test = await _testRepository.ReadWithQuestionsAsync(testResult.TestId);
        if (test == null)
        {
            throw new HttpException(HttpStatusCode.NotFound, "Test not found.");
        }

        int totalScore = 0;
        var questionScores = new List<QuestionScoreDto>();

        foreach (var answer in request.Request.Answers)
        {
            var question = test.Questions.FirstOrDefault(q => q.Id.Value == answer.QuestionId);
            if (question == null)
            {
                throw new HttpException(HttpStatusCode.BadRequest, $"Invalid question ID: {answer.QuestionId}");
            }

            bool isCorrect = question.AnswerOptions
                                 .Where(o => o.IsCorrect)
                                 .All(o => answer.SelectedOptionIds.Contains(o.Id)) &&
                             answer.SelectedOptionIds.All(id => question.AnswerOptions.Any(o => o.Id == id && o.IsCorrect));

            int pointsEarned = isCorrect ? question.PointValue : 0;
            totalScore += pointsEarned;

            questionScores.Add(new QuestionScoreDto
            {
                QuestionId = question.Id.Value,
                PointsEarned = pointsEarned
            });
        }

        testResult.UpdateScore(totalScore);
        await _testResultRepository.UpdateAsync(testResult);

        return new SubmitTestResponse
        {
            TotalScore = totalScore,
            QuestionScores = questionScores
        };
    }
}