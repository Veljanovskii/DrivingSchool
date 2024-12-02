using System.Net;
using DrivingSchool.Application.DataTransferObjects.UpdateTest;
using DrivingSchool.Application.Exceptions;
using DrivingSchool.Domain.Contracts;
using DrivingSchool.Domain.Entities;
using MediatR;

namespace DrivingSchool.Application.Features.UpdateTest;

public record UpdateTestCommand(UpdateTestRequest UpdateTestRequest) : IRequest<UpdateTestResponse>;

public class UpdateTestCommandHandler(ITestRepository testRepository,
    IUserRepository userRepository)
    : IRequestHandler<UpdateTestCommand, UpdateTestResponse>
{
    private readonly ITestRepository _testRepository = testRepository;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<UpdateTestResponse> Handle(UpdateTestCommand request, CancellationToken cancellationToken)
    {
        var test = await _testRepository.ReadAsync(new TestId(request.UpdateTestRequest.TestId));
        if (test == null)
        {
            throw new HttpException(HttpStatusCode.NotFound, "Test not found.");
        }

        User? user = await _userRepository.ReadAsync(new UserId(request.UpdateTestRequest.UserId));
        if (user == null)
        {
            throw new HttpException(HttpStatusCode.NotFound, "User not found.");
        }

        test.UpdateTitleAndDuration(user, request.UpdateTestRequest.Title, request.UpdateTestRequest.DurationInMinutes);

        foreach (var questionRequest in request.UpdateTestRequest.Questions)
        {
            var question = test.Questions.FirstOrDefault(q => q.Id.Value == questionRequest.Id);
            if (question == null)
            {
                question = Question.Create(questionRequest.Text, questionRequest.ImageUrl);
                test.AddQuestion(question);
            }
            else
            {
                question.UpdateTextAndImage(questionRequest.Text, questionRequest.ImageUrl);
            }

            foreach (var optionRequest in questionRequest.Options)
            {
                var option = question.AnswerOptions.FirstOrDefault(o => o.Id == optionRequest.Id);
                if (option == null)
                {
                    question.AddAnswerOption(new AnswerOption(optionRequest.Id, optionRequest.Text, optionRequest.IsCorrect));
                }
                else
                {
                    option.Update(optionRequest.Text, optionRequest.IsCorrect);
                }
            }
        }

        await _testRepository.UpdateAsync(test);

        return new UpdateTestResponse
        {
            TestId = test.Id.Value,
            Title = test.Title,
            DurationInMinutes = test.DurationInMinutes,
            TotalQuestions = test.Questions.Count
        };
    }
}