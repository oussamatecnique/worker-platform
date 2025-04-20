using worker.platform.application.Common;

namespace worker.platform.application.JobsDeals.Repositories;

public interface IJobRequestRepository: IRepositoryBase<JobRequest, int>
{
    public Task<IEnumerable<JobRequest>> GetPagedJobRequests(string city, int jobCategoryId, DateTime from, DateTime toDate, int page,
        int pageSize, CancellationToken cancellationToken = default
    );
}
