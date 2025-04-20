using worker.platform.application.Common.Models;

namespace worker.platform.application.Common;

public interface IRepositoryBase<T, TKey> where T: class
{
    public Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);

    public Task<IEnumerable<T>> GetPagedCursor(IQueryable<T> source,
        GetPagedKeySetQuery pageAndSortQuery,
        CancellationToken cancellationToken = default
    );

    public ValueTask<T?> FindAsync(TKey key, CancellationToken cancellationToken = default);

    public void Add(T newEntity);

    public void Update(T newEntity);

    public void Remove(T newEntity);

    public Task AddRangeAsync(IEnumerable<T> newEntities, CancellationToken cancellationToken = default);
}
