namespace worker.platform.application.JobsDeals.Mappers;

public interface IJobAssignmentMapper
{
    public JobRequest MapAddRequestDto(AddJobRequestDto addJobRequestDto);

    public domain.Entities.JobResponse MapJobApplicationToResponse(JobResponseDto jobResponseDto);
}
