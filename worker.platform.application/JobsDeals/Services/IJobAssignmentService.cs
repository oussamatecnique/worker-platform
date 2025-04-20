namespace worker.platform.application.JobsDeals.Services;

public interface IJobAssignmentService
{
    Task<bool> AddJobRequestAsync(AddJobRequestDto addJobRequestDto, CancellationToken cancellationToken);

    Task<bool> ApplyForJobRequestAsync(JobResponseDto jobResponseDto, CancellationToken cancellationToken);
}
