using worker.platform.application.JobsDeals.Repositories;
using worker.platform.domain.Entities;
using worker.platform.infrastructure.Common;

namespace worker.platform.infrastructure.JobsEntities;

public class JobResponseRepository(ApplicationDbContext applicationDbContext) : RepositoryBase<JobResponse, int>(applicationDbContext), IJobResponseRepository
{

}
