using DrivingSchool.Application.DataTransferObjects.CreateTest;
using DrivingSchool.Application.DataTransferObjects.UpdateTest;
using DrivingSchool.Application.Features.CreateTest;
using DrivingSchool.Application.Features.UpdateTest;
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
    public async Task<IActionResult> CreateTest([FromBody] CreateTestRequest request)
    {
        var command = new CreateTestCommand(request);
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTest([FromBody] UpdateTestRequest request)
    {
        var command = new UpdateTestCommand(request);
        var response = await _mediator.Send(command);
        return Ok(response);
    }
}