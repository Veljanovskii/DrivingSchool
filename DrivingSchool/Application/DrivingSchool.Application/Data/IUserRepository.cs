using DrivingSchool.Domain.Entities;

namespace DrivingSchool.Application.Data;

public interface IUserRepository
    : IRepository<UserId, User>
{
    Task<User?> GetUserByUsernameAsync(string userName);
}