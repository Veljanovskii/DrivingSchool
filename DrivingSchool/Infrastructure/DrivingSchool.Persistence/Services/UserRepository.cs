using DrivingSchool.Application.Data;
using DrivingSchool.Domain.Entities;
using DrivingSchool.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Persistence.Services;

internal class UserRepository(DrivingSchoolDbContext dbContext)
    : Repository<UserId, User, DrivingSchoolDbContext>(dbContext, dbContext.Users)
    , IUserRepository
{
    protected sealed override UserId GetKey(User user)
    {
        return user.Id;
    }

    public async Task<User?> GetUserByUsernameAsync(string userName)
    {
        return await dbContext.Users.FirstOrDefaultAsync(u => u.Username == userName);
    }
}