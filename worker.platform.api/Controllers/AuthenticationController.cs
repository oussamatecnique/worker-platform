using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using worker.platform.application.Users.DTOs;
using worker.platform.application.Users.Services;

namespace worker.platform.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthenticationController(
    ILogger<AuthenticationController> logger,
    IAuthenticationService authenticationService
)
    : ControllerBase
{
    private readonly ILogger<AuthenticationController> _logger =
        logger ?? throw new ArgumentNullException(nameof(logger));

    private readonly IAuthenticationService _authenticationService =
        authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));

    [HttpPost("login")]
    public async Task<ActionResult<string>> SignIn([FromBody] SignInUserDto userDto)
    {

        var token = await _authenticationService.SignInAsync(userDto.Email, userDto.Password);

        return Ok(token);
    }

    [HttpPost("signup")]
    public async Task<ActionResult<string>> SignUp([FromBody] SignUpUserDto userDto)
    {
        _logger.Log(LogLevel.Information,"Signing up {user}", JsonSerializer.Serialize(userDto));
        var signedUp = await _authenticationService.SignUpAsync(userDto);

        return Ok(signedUp);
    }

    [HttpPost("worker-profile")]
    public async Task<ActionResult<string>> WorkerProfile([FromBody] WorkerProfileDto workerProfileDto)
    {
        var updated = await _authenticationService.SetupWorkerProfile(workerProfileDto);

        return Ok(updated);
    }
}
