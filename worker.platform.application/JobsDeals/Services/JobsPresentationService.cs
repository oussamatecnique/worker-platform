using System.Globalization;
using worker.platform.application.Common.Caching;
using worker.platform.application.Common.Exceptions;
using worker.platform.application.Common.Static;
using worker.platform.application.JobsDeals.Repositories;
using worker.platform.application.Users.Repositories;

namespace worker.platform.application.JobsDeals.Services;

public class JobsPresentationService: IJobsPresentationService
{
    private readonly IWorkerRepository _workerRepository;
    private readonly ICacheRepository _cacheRepository;
    private readonly IJobRequestRepository _jobRequestRepository;

    public JobsPresentationService(IWorkerRepository workerRepository, IJobRequestRepository jobRequestRepository, ICacheRepository cacheRepository)
    {
        _workerRepository = workerRepository ?? throw new ArgumentNullException(nameof(workerRepository));
        _jobRequestRepository = jobRequestRepository ?? throw new ArgumentNullException(nameof(jobRequestRepository));
        _cacheRepository = cacheRepository  ?? throw new ArgumentNullException(nameof(cacheRepository));
    }

    public async Task<IEnumerable<JobRequest>> GetJobRequestsAsync(GetJobRequestsQuery query)
    {
        var worker = await  _cacheRepository.GetOrSetAsync<Worker>(string.Format(CultureInfo.InvariantCulture, Constants.Cache.WorkerCacheKey, query.WorkerId),
            async (_) => await _workerRepository.GetDetailedWorkerById(query.WorkerId, CancellationToken.None));
        
        _ = worker ?? throw new NotFoundException(nameof(worker));

        var jobRequests = await _jobRequestRepository.GetPagedJobRequests(worker.City.Name, worker.JobCategoryId,
            query.From, query.To, query.PageNumber, query.PageSize, CancellationToken.None);

        return jobRequests;
    }
}
