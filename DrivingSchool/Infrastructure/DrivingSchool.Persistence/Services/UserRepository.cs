using DrivingSchool.Domain.Contracts;
using DrivingSchool.Domain.Entities;
using DrivingSchool.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Persistence.Services;

internal class UserRepository(DrivingSchoolDbContext dbContext)
    : Repository<UserId, User, DrivingSchoolDbContext>(dbContext, dbContext.Users)
    , IUserRepository
{
    public async Task<User?> GetUserByUsernameAsync(string userName)
    {
        return await dbContext.Users.FirstOrDefaultAsync(u => u.Username == userName);
    }
}