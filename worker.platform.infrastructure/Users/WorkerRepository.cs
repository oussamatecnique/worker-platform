using CacheTower;
using Microsoft.EntityFrameworkCore;
using worker.platform.application.Common.Caching;
using worker.platform.application.Common.Static;
using worker.platform.application.Users.Repositories;
using worker.platform.domain.Entities;
using worker.platform.infrastructure.Common;

namespace worker.platform.infrastructure.Users;

public class WorkerRepository(ApplicationDbContext applicationDbContext, ICacheRepository cacheRepository)
    : RepositoryBase<Worker, int>(applicationDbContext), IWorkerRepository
{
    public async ValueTask<Worker> GetDetailedWorkerById(int id, CancellationToken cancellationToken = default)
    {
        return await DbSet.Include(worker => worker.City).Include(worker => worker.JobCategory)
            .Include(worker => worker.User).Include(worker => worker.JobAssignments)
            .AsSplitQuery().FirstOrDefaultAsync(cancellationToken);
    }
}
