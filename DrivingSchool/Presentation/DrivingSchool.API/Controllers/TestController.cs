using DrivingSchool.Application.DataTransferObjects.CreateTest;
using DrivingSchool.Application.Features.CreateTest;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DrivingSchool.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    //[Authorize(Roles = "Moderator")]
    public async Task<IActionResult> CreateTest([FromBody] CreateTestRequest request)
    {
        var command = new CreateTestCommand(request);
        var response = await _mediator.Send(command);
        return Ok(response);
    }
}