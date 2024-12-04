using DrivingSchool.Application.DataTransferObjects.StartTest;
using DrivingSchool.Domain.Contracts;
using DrivingSchool.Domain.Entities;
using MediatR;

namespace DrivingSchool.Application.Features.StartTest;

public record StartTestCommand(StartTestRequest Request) : IRequest<StartTestResponse>;

public class StartTestCommandHandler(ITestResultRepository testResultRepository)
    : IRequestHandler<StartTestCommand, StartTestResponse>
{
    private readonly ITestResultRepository _testResultRepository = testResultRepository;

    public async Task<StartTestResponse> Handle(StartTestCommand request, CancellationToken cancellationToken)
    {
        var testResult = TestResult.Create(
            new TestId(request.Request.TestId),
            new UserId(request.Request.CandidateId), 
            0);

        await _testResultRepository.CreateAsync(testResult);

        return new StartTestResponse
        {
            TestResultId = testResult.TestId.Value,
            TakenAt = testResult.TakenAt
        };
    }
}
