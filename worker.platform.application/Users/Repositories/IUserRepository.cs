using worker.platform.application.Common;
using worker.platform.application.Common.Models;

namespace worker.platform.application.Users.Repositories;

public interface IUserRepository: IRepositoryBase<User, int>
{
    public ValueTask<User?> GetUserById(int userId);

    public ValueTask<User?> GetByEmailAsync(string email);

    public Task<PagedResultModel<User>> GetPagedUsersAsync(GetPagedKeySetQuery query);

    public void AddOrUpdateWorkerProfile(Worker worker);
}
