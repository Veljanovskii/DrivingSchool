using DrivingSchool.Application.Data;
using DrivingSchool.Application.DataTransferObjects.Access;
using DrivingSchool.Domain.Entities;
using MediatR;

namespace DrivingSchool.Application.Features.Access;

public record LoginCommand(string Username, string Password) : IRequest<LoginResponse>;

public class LoginCommandHandler(IUserRepository userRepository) : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        // Simulate fetching user details from the repository
        var user = await _userRepository.GetUserByUsernameAsync(request.Username);

        if (user == null || user.Password != request.Password)
        {
            return null; // You may want to throw an exception or return an error response here
        }

        // Generate a token or session (simulated here)
        var token = "simulated-jwt-token"; // Replace with real JWT generation logic

        var response = new LoginResponse
        {
            UserId = user.Id.ToString(),
            UserRole = user.GetType().Name, // Assuming roles are derived from user type (Candidate, Moderator)
            Token = token
        };

        return response;
    }
}