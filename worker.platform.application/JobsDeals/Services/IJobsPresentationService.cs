namespace worker.platform.application.JobsDeals.Services;

public interface IJobsPresentationService
{
    public Task<IEnumerable<JobRequest>> GetJobRequestsAsync(GetJobRequestsQuery query);
}
