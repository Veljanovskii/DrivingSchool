using DrivingSchool.Application.Data;
using DrivingSchool.Application.DataTransferObjects.Access;
using DrivingSchool.Application.Exceptions;
using MediatR;

namespace DrivingSchool.Application.Features.Access;

public record LoginCommand(LoginRequest LoginRequest) : IRequest<LoginResponse>;

public class LoginCommandHandler(IUserRepository userRepository) : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByUsernameAsync(request.LoginRequest.Username);

        if (user == null)
            throw new HttpException(System.Net.HttpStatusCode.NotFound, "User not found.");

        if (user.Password != request.LoginRequest.Password)
            throw new HttpException(System.Net.HttpStatusCode.BadRequest, "Password does not match.");

        var token = "simulated-jwt-token";

        var response = new LoginResponse
        {
            UserId = user.Id.ToString(),
            UserRole = user.GetType().Name,
            Token = token
        };

        return response;
    }
}