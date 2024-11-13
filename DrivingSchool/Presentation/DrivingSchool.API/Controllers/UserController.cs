using DrivingSchool.Application.DataTransferObjects.Access;
using DrivingSchool.Application.Features.Access;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DrivingSchool.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IMediator mediator) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var command = new LoginCommand(loginRequest);
        var response = await mediator.Send(command);
        return Ok(response);
    }
}
