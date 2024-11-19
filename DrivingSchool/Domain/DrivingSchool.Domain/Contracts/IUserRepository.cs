using DrivingSchool.Domain.Entities;

namespace DrivingSchool.Domain.Contracts;

public interface IUserRepository
    : IRepository<UserId, User>
{
    Task<User?> GetUserByUsernameAsync(string userName);
}