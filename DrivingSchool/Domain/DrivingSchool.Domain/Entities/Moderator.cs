namespace DrivingSchool.Domain.Entities;

public record Moderator : User
{
    private Moderator(UserId id, string username, string password)
        : base(id, username, password) { }

    public override bool CanManageTests() => true;

    public static Moderator Create(string username, string password)
    {
        return new Moderator(new UserId(Guid.NewGuid()), username, password);
    }
}
