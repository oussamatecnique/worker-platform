using worker.platform.application.Common;

namespace worker.platform.application.Users.Repositories;

public interface IWorkerRepository: IRepositoryBase<Worker, int>
{
    public ValueTask<Worker?> GetDetailedWorkerById(int id, CancellationToken cancellationToken = default);
}
