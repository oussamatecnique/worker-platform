using worker.platform.application.JobsDeals.Repositories;
using worker.platform.application.Users.Repositories;

namespace worker.platform.application.JobsDeals.Validators;

public class JobAssignmentValidator: IJobAssignmentValidator
{
    private readonly ICacheWorkerRepository _cacheWorkerRepository;
    private readonly ICacheTaskTypeParamsDefinitionRepository _taskTypeParamsDefinitionRepository;

    public JobAssignmentValidator(ICacheWorkerRepository cacheWorkerRepository, ICacheTaskTypeParamsDefinitionRepository taskTypeParamsDefinitionRepository)
    {
        _cacheWorkerRepository = cacheWorkerRepository ?? throw new ArgumentNullException(nameof(cacheWorkerRepository));
        _taskTypeParamsDefinitionRepository = taskTypeParamsDefinitionRepository ?? throw new ArgumentNullException(nameof(taskTypeParamsDefinitionRepository));
    }

    public async Task<(bool, string)> ValidateJobResponseAsync(JobResponseDto jobResponseDto)
    {
        var worker = await _cacheWorkerRepository.GetCachedWorker(
            jobResponseDto.WorkerId,
            CancellationToken.None);

        if (worker == null) return (false, "Invalid worker Id");

        // Store validation results
        var isValidCity = worker.CityId == jobResponseDto.CityId;
        var hasNoPendingJobs = worker.JobAssignments.All(j => j.Status == ServiceJobStatus.Done);

        if (isValidCity && hasNoPendingJobs)
        {
            return (true, null);
        }
        return (false, "invalid city or worker has pending jobs");
    }

    public async Task<(bool, string)> ValidateJobRequestAsync(AddJobRequestDto addJobRequestDto)
    {
        var paramsDtoNames = addJobRequestDto.Params.JobRequestParamsValues.Select(y => y.AttributeName).Distinct();
        var dboCategoryParams =
            await _taskTypeParamsDefinitionRepository.GetCacheAttributesByTaskTypeId(addJobRequestDto.TaskTypeId);

        if (paramsDtoNames.All(dtoAttribute => dboCategoryParams.Contains(dtoAttribute)))
        {
            return (true, null);
        }

        return (false, "invalid task type");
    }
}
