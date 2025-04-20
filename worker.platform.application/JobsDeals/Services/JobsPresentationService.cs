using CacheTower;
using worker.platform.application.Common.Exceptions;
using worker.platform.application.Common.Static;
using worker.platform.application.JobsDeals.Repositories;
using worker.platform.application.Users.Repositories;

namespace worker.platform.application.JobsDeals.Services;

public class JobsPresentationService: IJobsPresentationService
{
    private readonly IWorkerRepository _workerRepository;
    private readonly IJobRequestRepository _jobRequestRepository;

    public JobsPresentationService(IWorkerRepository workerRepository, IJobRequestRepository jobRequestRepository)
    {
        _workerRepository = workerRepository ?? throw new ArgumentNullException(nameof(workerRepository));
        _jobRequestRepository = jobRequestRepository ?? throw new ArgumentNullException(nameof(jobRequestRepository));
    }

    public async Task<IEnumerable<JobRequest>> GetJobRequestsAsync(GetJobRequestsQuery query)
    {
        var worker = await _workerRepository.GetDetailedWorkerById(query.WorkerId) ?? throw new NotFoundException();


        var jobRequests = await _jobRequestRepository.GetPagedJobRequests(worker.City.Name, worker.JobCategoryId,
            query.From, query.To, query.PageNumber, query.PageSize, CancellationToken.None);

        return jobRequests;
    }
}
