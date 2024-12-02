using DrivingSchool.Domain.Entities;

namespace DrivingSchool.Domain.Contracts;

public interface ITestRepository : IRepository<TestId, Test>
{
}