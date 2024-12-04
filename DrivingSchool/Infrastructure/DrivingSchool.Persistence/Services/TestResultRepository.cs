using DrivingSchool.Domain.Contracts;
using DrivingSchool.Domain.Entities;
using DrivingSchool.Persistence.Context;

namespace DrivingSchool.Persistence.Services;

internal class TestResultRepository(DrivingSchoolDbContext dbContext)
    : Repository<TestId, TestResult, DrivingSchoolDbContext>(dbContext, dbContext.TestResults), ITestResultRepository
{
}