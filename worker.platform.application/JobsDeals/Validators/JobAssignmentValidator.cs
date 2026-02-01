using System.Globalization;
using worker.platform.application.Common.Caching;
using worker.platform.application.Common.Static;
using worker.platform.application.JobsDeals.Repositories;
using worker.platform.application.Users.Repositories;

namespace worker.platform.application.JobsDeals.Validators;

public class JobAssignmentValidator: IJobAssignmentValidator
{
    private readonly ICacheRepository _cacheRepository;
    private readonly IWorkerRepository _workerRepository;
    private readonly ITaskTypeParamDefinitionRepository _taskTypeParamsDefinitionRepository;

    public JobAssignmentValidator(ICacheRepository cacheRepository, ITaskTypeParamDefinitionRepository taskTypeParamsDefinitionRepository, 
        IWorkerRepository workerRepository)
    {
        _cacheRepository = cacheRepository ?? throw new ArgumentNullException(nameof(cacheRepository));
        _taskTypeParamsDefinitionRepository = taskTypeParamsDefinitionRepository ?? throw new ArgumentNullException(nameof(taskTypeParamsDefinitionRepository));
        _workerRepository = workerRepository  ?? throw new ArgumentNullException(nameof(workerRepository));
    }

    public async Task<(bool, string)> ValidateJobResponseAsync(JobResponseDto jobResponseDto)
    {
        var worker = await  _cacheRepository.GetOrSetAsync<Worker>(string.Format(CultureInfo.InvariantCulture, Constants.Cache.WorkerCacheKey, jobResponseDto.WorkerId),
            async (_) => await _workerRepository.GetDetailedWorkerById(jobResponseDto.WorkerId, CancellationToken.None));


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
        var dboCategoryParams = await _cacheRepository.GetOrSetAsync<IEnumerable<string>>($"params-{addJobRequestDto.TaskTypeId}",
            (old) => Task.FromResult(_taskTypeParamsDefinitionRepository.GetAttributesByCategoryId(addJobRequestDto.TaskTypeId)));

        if (paramsDtoNames.All(dtoAttribute => dboCategoryParams.Contains(dtoAttribute)))
        {
            return (true, null);
        }

        return (false, "invalid task type");
    }
}
