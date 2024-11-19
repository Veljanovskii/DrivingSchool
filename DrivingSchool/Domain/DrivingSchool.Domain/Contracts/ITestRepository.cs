using DrivingSchool.Domain.Entities;

namespace DrivingSchool.Domain.Contracts;

public interface ITestRepository : IRepository<Guid, Test>
{
    Task<List<Test>> GetAllTestsWithQuestionsAsync();
}