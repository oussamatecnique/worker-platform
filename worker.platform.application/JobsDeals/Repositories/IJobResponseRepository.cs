using worker.platform.application.Common;
using JobResponse = worker.platform.domain.Entities.JobResponse;

namespace worker.platform.application.JobsDeals.Repositories;

public interface IJobResponseRepository: IRepositoryBase<JobResponse, int>
{

}
