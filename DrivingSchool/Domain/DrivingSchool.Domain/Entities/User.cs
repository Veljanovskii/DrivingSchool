namespace DrivingSchool.Domain.Entities;

public record UserId(Guid Value);

public abstract record User
{
    public UserId Id { get; init; }
    public string Username { get; init; }
    public string Password { get; init; }

    protected User(UserId id, string username, string password)
    {
        Id = id;
        Username = username;
        Password = password;
    }

    public abstract bool CanManageTests();
}
