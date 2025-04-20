using System.Globalization;
using worker.platform.application.Common.Caching;
using worker.platform.application.Common.Static;
using worker.platform.application.Users.Repositories;
using worker.platform.domain.Entities;

namespace worker.platform.infrastructure.Users;

public class CacheWorkerRepository: ICacheWorkerRepository
{
    private readonly ICacheRepository _cacheRepository;
    private readonly IWorkerRepository _workerRepository;

    public CacheWorkerRepository(ICacheRepository cacheRepository)
    {
        _cacheRepository = cacheRepository ?? throw new ArgumentNullException(nameof(cacheRepository));
    }

    public ValueTask<Worker> GetCachedWorker(int id, CancellationToken cancellationToken = default)
    {
        return _cacheRepository.GetOrSetAsync<Worker?>(string.Format(CultureInfo.InvariantCulture, Constants.Cache.WorkerCacheKey, id),
            async (old) => await _workerRepository.GetDetailedWorkerById(id, cancellationToken));
    }
}
