namespace worker.platform.application.JobsDeals.Validators;

public interface IJobAssignmentValidator
{
    Task<(bool, string)> ValidateJobResponseAsync(JobResponseDto jobResponseDto);

    Task<(bool, string)> ValidateJobRequestAsync(AddJobRequestDto addJobRequestDto);
}
