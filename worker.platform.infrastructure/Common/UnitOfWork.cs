using worker.platform.application.Common;

namespace worker.platform.infrastructure.Common;

public class UnitOfWork : IUnitOfWork
{
    private ApplicationDbContext _applicationDbContext;

    public UnitOfWork(ApplicationDbContext context)
    {
        _applicationDbContext = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await _applicationDbContext.SaveChangesAsync(cancellationToken);

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default) =>
       await _applicationDbContext.Database.BeginTransactionAsync(cancellationToken);

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default) =>
        await _applicationDbContext.Database.CommitTransactionAsync(cancellationToken);

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default) =>
        await _applicationDbContext.Database.RollbackTransactionAsync(cancellationToken);
}
