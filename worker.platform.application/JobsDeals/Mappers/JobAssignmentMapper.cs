using Riok.Mapperly.Abstractions;

namespace worker.platform.application.JobsDeals.Mappers;

[Mapper]
public partial class JobAssignmentMapper: IJobAssignmentMapper
{
    [MapProperty(nameof(AddJobRequestDto.Params), nameof(JobRequest.Params))]
    public partial JobRequest MapAddRequestDto(AddJobRequestDto addJobRequestDto);

    public partial domain.Entities.JobResponse MapJobApplicationToResponse(JobResponseDto jobResponseDto);
}
