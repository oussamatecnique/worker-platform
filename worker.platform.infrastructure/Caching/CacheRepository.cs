using CacheTower;
using worker.platform.application.Common.Caching;

namespace worker.platform.infrastructure.Caching;

public class CacheRepository: ICacheRepository
{
    private readonly ICacheStack _cacheStack;

    public CacheRepository(ICacheStack cacheStack)
    {
        _cacheStack = cacheStack ?? throw new ArgumentNullException(nameof(cacheStack));
    }

    public ValueTask<T> GetOrSetAsync<T>(string key, Func<T, Task<T>> factory) =>
        _cacheStack.GetOrSetAsync<T>(key, async (old) =>
        {
            return await factory(old);
        }, new());
}
