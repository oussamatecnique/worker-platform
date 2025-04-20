namespace worker.platform.application.Common.Caching;

public interface ICacheRepository
{
    public ValueTask<T> GetOrSetAsync<T>(string key, Func<T, Task<T>> factory);
}
