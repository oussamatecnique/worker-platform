namespace worker.platform.application.Users.Repositories;

public interface ICacheWorkerRepository
{
    public ValueTask<Worker> GetCachedWorker(int id, CancellationToken cancellationToken = default);

}
