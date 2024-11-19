using DrivingSchool.Domain.Contracts;
using DrivingSchool.Domain.Entities;
using DrivingSchool.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Persistence.Services;

internal class TestRepository(DrivingSchoolDbContext dbContext)
    : Repository<Guid, Test, DrivingSchoolDbContext>(dbContext, dbContext.Tests), ITestRepository
{
    public async Task<List<Test>> GetAllTestsWithQuestionsAsync()
    {
        return await Read(_ => true)
            .Include(t => t.Questions)
            .ToListAsync();
    }
}