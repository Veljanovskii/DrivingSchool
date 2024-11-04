namespace DrivingSchool.Application.DataTransferObjects.Access;

public class LoginResponse
{
    public string Token { get; set; }
    public string UserId { get; set; }
    public string UserRole { get; set; }
}