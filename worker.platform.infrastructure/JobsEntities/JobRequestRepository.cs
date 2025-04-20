using worker.platform.application.JobsDeals.Repositories;
using worker.platform.domain.Entities;
using worker.platform.infrastructure.Common;

namespace worker.platform.infrastructure.JobsEntities;

public class JobRequestRepository(ApplicationDbContext applicationDbContext) : RepositoryBase<JobRequest, int>(applicationDbContext), IJobRequestRepository
{
    public Task<IEnumerable<JobRequest>> GetPagedJobRequests(string city,int jobCategoryId, DateTime from, DateTime toDate, int page,
        int pageSize,
        CancellationToken cancellationToken = default
    )
    {
        var query = DbSet.Where(x => x.City.Name == city)
            .AsQueryable()
            .Where(x => x.StartDate >= from)
            .Where(x => x.StartDate <= toDate)
            .Where(x => x.TaskTypeId == jobCategoryId);

        // appy page
        query = query.Skip((page - 1) * pageSize).Take(pageSize);
        query = query.OrderByDescending(x => x.StartDate);

        return Task.FromResult(query.AsEnumerable());
    }
}
