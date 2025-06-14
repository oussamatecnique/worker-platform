using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using worker.platform.application.BackOffice;
using worker.platform.application.BackOffice.DTOs;
using worker.platform.application.Users.Services;

namespace worker.platform.Controllers;

[ApiController]
[Route("api/bo")]
[Authorize("admin")]
public class BackOfficeController : BaseAuthenticatedUserController
{
    private readonly ILogger<BackOfficeController> _logger;
    private readonly IBoService _boService;

    public BackOfficeController(IAuthenticationService authenticationService, IHttpContextAccessor httpContextAccessor,
        ILogger<BackOfficeController> logger, IBoService boService
    ) : base(authenticationService, httpContextAccessor)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _boService = boService ?? throw new ArgumentNullException(nameof(boService));
    }

    [HttpPost("users")]
    public async Task<IActionResult> GetUsers([FromBody] GetPagedUsersDto query, CancellationToken token)
    {

        var users = await _boService.GetPagedUsersAsync(query, token);

        return Ok(users);
    }

    [HttpPut("add-task-type-params")]
    public async Task<IActionResult> AddJobCategoryPrams(AddTaskTypeParamsDefinitionDto addTaskTypeParamsDefinitionDto)
    {
        _logger.LogInformation($"Adding job category params : {addTaskTypeParamsDefinitionDto}, by {CurrentUser.Result?.Email}");

        var result = await _boService.CreateTaskParamsDefinitionAsync(addTaskTypeParamsDefinitionDto);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddTaskType(AddTaskTypeDto addTaskTypeDto, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Adding task type : {addTaskTypeDto}");
        var result = await _boService.CreateTaskTypeAsync(addTaskTypeDto, cancellationToken);

        return Ok(result);
    }

    [HttpPost("add-admin-user")]
    public async Task<IActionResult> AddAdminUser([FromBody] AddAdminUser data, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Adding an admin user {data.Email}, by {CurrentUser.Result?.Email}");

        var result = await _boService.AddAdminUserAsync(data, cancellationToken);

        return Ok(result);

    }
}
