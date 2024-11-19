using DrivingSchool.Application.DataTransferObjects.Access;
using DrivingSchool.Application.Exceptions;
using DrivingSchool.Domain.Contracts;
using DrivingSchool.Domain.Entities;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

        var token = GenerateJwtToken(user);

        var response = new LoginResponse
        {
            UserId = user.Id.ToString(),
            UserRole = user.GetType().Name,
            Token = token
        };

        return response;
    }

    private string GenerateJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSuperSecretKeyYourSuperSecretKey"));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.GetType().Name),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: "DrivingSchool",
            audience: "DrivingSchool",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}