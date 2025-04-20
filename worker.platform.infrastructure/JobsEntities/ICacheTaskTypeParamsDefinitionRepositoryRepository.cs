using CacheTower;
using worker.platform.application.Common.Caching;
using worker.platform.application.JobsDeals.Repositories;

namespace worker.platform.infrastructure.JobsEntities;

public class CacheTaskTypeParamsDefinitionRepositoryRepository : ICacheTaskTypeParamsDefinitionRepository
{
    private readonly ICacheRepository _cacheRepository;
    private readonly ITaskTypeParamDefinitionRepository _taskTypeParamDefinitionRepository;

    public CacheTaskTypeParamsDefinitionRepositoryRepository(ICacheRepository cacheRepository,
        ITaskTypeParamDefinitionRepository taskTypeParamDefinitionRepository
    )
    {
        _cacheRepository = cacheRepository ?? throw new ArgumentNullException(nameof(cacheRepository));
        _taskTypeParamDefinitionRepository = taskTypeParamDefinitionRepository ??
                                                throw new ArgumentNullException(
                                                    nameof(taskTypeParamDefinitionRepository));
    }

    public ValueTask<IEnumerable<string>> GetCacheAttributesByTaskTypeId(int categoryId)
    {
        return _cacheRepository.GetOrSetAsync<IEnumerable<string>>($"params-{categoryId}",
            (old) => Task.FromResult(_taskTypeParamDefinitionRepository.GetAttributesByCategoryId(categoryId)));
    }
}
