using DrivingSchool.Domain.Contracts;
using DrivingSchool.Domain.Entities;
using DrivingSchool.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Persistence.Services;

internal class TestRepository(DrivingSchoolDbContext dbContext)
    : Repository<TestId, Test, DrivingSchoolDbContext>(dbContext, dbContext.Tests), ITestRepository
{
}