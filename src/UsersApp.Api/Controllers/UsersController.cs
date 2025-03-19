using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersApp.Application.Commands.Users;
using UsersApp.Application.DTO.Users;
using UsersApp.Application.Queries.Users;
using UsersApp.Application.Services;

namespace UsersApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ITokenStorage _tokenStorage;

    public UsersController(IMediator mediator,
        ITokenStorage tokenStorage)
    {
        _mediator = mediator;
        _tokenStorage = tokenStorage;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> Get([FromQuery] GetUsers request)
        => Ok(await _mediator.Send(request));

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteUser(id));
        return NoContent();
    }

    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put(Guid id, ChangeUserName request)
    {
        await _mediator.Send(request with
        {
            UserId = id
        });
        return NoContent();
    }
    
    [HttpPost]
    public async Task<IActionResult> Post(SignUp command)
    {
        command = command with { UserId = Guid.NewGuid() };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> Post(SignIn command)
    {
        await _mediator.Send(command);
        var jwt = _tokenStorage.Get();
        return Ok(jwt);
    }
}