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

        var test = await _testRepository.ReadWithQuestionsAsync(new TestId(testResult.TestId.Value));
        if (test == null)
        {
            throw new HttpException(HttpStatusCode.NotFound, "Test not found.");
        }

        int totalScore = 0;

        foreach (var answer in request.Request.Answers)
        {
            var question = test.Questions.FirstOrDefault(q => q.Id.Value == answer.QuestionId);
            if (question == null)
            {
                throw new HttpException(HttpStatusCode.NotFound, $"Question with ID {answer.QuestionId} not found.");
            }

            var correctOptions = question.AnswerOptions.Where(o => o.IsCorrect).Select(o => o.Id).ToList();
            if (!correctOptions.Except(answer.SelectedOptionIds).Any() &&
                !answer.SelectedOptionIds.Except(correctOptions).Any())
            {
                totalScore += question.PointValue;
            }
        }

        testResult.UpdateScore(totalScore);
        await _testResultRepository.UpdateAsync(testResult);

        return new SubmitTestResponse { TotalScore = totalScore };
    }
}