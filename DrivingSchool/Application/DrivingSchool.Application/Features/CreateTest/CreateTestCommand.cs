using System.Net;
using DrivingSchool.Application.DataTransferObjects.CreateTest;
using DrivingSchool.Application.Exceptions;
using DrivingSchool.Domain.Contracts;
using DrivingSchool.Domain.Entities;
using MediatR;

namespace DrivingSchool.Application.Features.CreateTest;

public record CreateTestCommand(CreateTestRequest CreateTestRequest) : IRequest<CreateTestResponse>;

public class CreateTestCommandHandler(ITestRepository testRepository)
    : IRequestHandler<CreateTestCommand, CreateTestResponse>
{
    private readonly ITestRepository _testRepository = testRepository;

    public async Task<CreateTestResponse> Handle(CreateTestCommand request, CancellationToken cancellationToken)
    {
        if (!string.Equals(request.CreateTestRequest.UserType, "Moderator", StringComparison.OrdinalIgnoreCase))
        {
            throw new HttpException( HttpStatusCode.Forbidden, "Only moderators can create tests.");
        }

        var test = Test.Create(request.CreateTestRequest.Title, request.CreateTestRequest.DurationInMinutes);

        foreach (var questionDto in request.CreateTestRequest.Questions)
        {
            var question = Question.Create(
                questionDto.Text,
                questionDto.ImageUrl
            );

            foreach (var option in questionDto.Options)
            {
                var answerOption = AnswerOption.Create(
                    option.Text,
                    option.IsCorrect
                );

                question.AddAnswerOption(answerOption);
            }

            test.AddQuestion(question);
        }

        await _testRepository.CreateAsync(test);

        return new CreateTestResponse
        {
            TestId = test.Id.Value,
            Title = test.Title,
            DurationInMinutes = test.DurationInMinutes,
            TotalQuestions = test.Questions.Count
        };
    }
}
