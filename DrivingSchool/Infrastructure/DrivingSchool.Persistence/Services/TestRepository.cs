using DrivingSchool.Domain.Contracts;
using DrivingSchool.Domain.Entities;
using DrivingSchool.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Persistence.Services;

internal class TestRepository(DrivingSchoolDbContext dbContext)
    : Repository<TestId, Test, DrivingSchoolDbContext>(dbContext, dbContext.Tests), ITestRepository
{
    public async Task<Test?> ReadWithQuestionsAsync(TestId id)
    {
        var test = await DbContext.Tests.FirstOrDefaultAsync(t => t.Id == id);

        if (test != null)
        {
            await DbContext.Entry(test)
                .Collection(t => t.Questions)
                .LoadAsync();

            foreach (var question in test.Questions)
            {
                await DbContext.Entry(question)
                    .Collection(q => q.AnswerOptions)
                    .LoadAsync();
            }
        }

        return test;
    }
}