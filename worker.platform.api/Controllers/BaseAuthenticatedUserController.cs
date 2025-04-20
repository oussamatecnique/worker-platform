using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using worker.platform.application.Users.Services;
using worker.platform.domain.Entities;

namespace worker.platform.Controllers;

public class BaseAuthenticatedUserController: ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public BaseAuthenticatedUserController(IAuthenticationService authenticationService, IHttpContextAccessor httpContextAccessor)
    {
        _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        _httpContextAccessor = httpContextAccessor;
    }
    // get userid from http context
    private Task<User?>? _currentUserTask;

    public Task<User?> CurrentUser => _currentUserTask ??= GetCurrentUserAsync();
    [NonAction]
    public async Task<User?> GetCurrentUserAsync()
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return null; // Or throw an exception if needed
        }

        return await _authenticationService.GetUserByIdAsync(int.Parse(userId, CultureInfo.InvariantCulture));
    }

}
