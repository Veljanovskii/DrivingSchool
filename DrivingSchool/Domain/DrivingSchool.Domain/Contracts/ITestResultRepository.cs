using DrivingSchool.Domain.Entities;

namespace DrivingSchool.Domain.Contracts;

public interface ITestResultRepository : IRepository<TestId, TestResult>
{
}