using System.Net;
using DrivingSchool.Application.DataTransferObjects.CreateTest;
using DrivingSchool.Application.Exceptions;
using DrivingSchool.Domain.Contracts;
using DrivingSchool.Domain.Entities;
using MediatR;

namespace DrivingSchool.Application.Features.CreateTest;

public record CreateTestCommand(CreateTestRequest CreateTestRequest) : IRequest<CreateTestResponse>;

public class CreateTestCommandHandler(ITestRepository testRepository,
    IUserRepository userRepository)
    : IRequestHandler<CreateTestCommand, CreateTestResponse>
{
    private readonly ITestRepository _testRepository = testRepository;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<CreateTestResponse> Handle(CreateTestCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.ReadAsync(new UserId(request.CreateTestRequest.UserId));
        if (user == null)
        {
            throw new HttpException(HttpStatusCode.NotFound, "User not found.");
        }

        var test = Test.Create(user, request.CreateTestRequest.Title, request.CreateTestRequest.DurationInMinutes);

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
