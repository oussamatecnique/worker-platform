using Microsoft.AspNetCore.Mvc;
using worker.platform.application.JobsDeals.DTOs;
using worker.platform.application.JobsDeals.Services;
using worker.platform.application.Users.Services;

namespace worker.platform.Controllers;

[ApiController]
[Route("api/job-assigning")]
public class JobsAssigningController: BaseAuthenticatedUserController
{
    private readonly ILogger<JobsAssigningController> _logger;
    private readonly IJobsPresentationService _jobsPresentationService;
    private readonly IJobAssignmentService _jobAssignmentService;

    public JobsAssigningController(IAuthenticationService authenticationService,
        IHttpContextAccessor httpContextAccessor, ILogger<JobsAssigningController> logger,
        IJobsPresentationService jobsPresentationService, IJobAssignmentService jobAssignmentService
    ) : base(authenticationService, httpContextAccessor)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _jobsPresentationService =
            jobsPresentationService ?? throw new ArgumentNullException(nameof(jobsPresentationService));
        _jobAssignmentService = jobAssignmentService ?? throw new ArgumentNullException(nameof(jobAssignmentService));
    }

    [HttpPost("add-job-request")]
    public async Task<IActionResult> AddJobRequest(AddJobRequestDto data, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Add job request");
        var result = await _jobAssignmentService.AddJobRequestAsync(data, cancellationToken);

        return Ok(result);
    }

    [HttpPost("add-job-response")]
    public async Task<IActionResult> AddJobResponse(JobResponseDto data, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"worker {CurrentUser.Result.Id} add job response");
        var result = await _jobAssignmentService.ApplyForJobRequestAsync(data, cancellationToken);

        return Ok(result);
    }
}
