using CacheTower;
using Microsoft.EntityFrameworkCore;
using worker.platform.application.Common.Caching;
using worker.platform.application.Common.Models;
using worker.platform.application.Users.Repositories;
using worker.platform.domain.Entities;
using worker.platform.infrastructure.Common;

namespace worker.platform.infrastructure.Users;

public class UserRepository(ApplicationDbContext applicationDbContext, ICacheRepository cacheRepository)
    : RepositoryBase<User, int>(applicationDbContext), IUserRepository
{
    private readonly ApplicationDbContext _applicationDbContext =
        applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));

    private readonly ICacheRepository _cacheRepository =
        cacheRepository ?? throw new ArgumentNullException(nameof(cacheRepository));


    public async ValueTask<User?> GetByEmailAsync(string email) =>
        await DbSet.FirstOrDefaultAsync(x => x.Email == email);

    public async Task<PagedResultModel<User>> GetPagedUsersAsync(GetPagedKeySetQuery query)
    {
        var sourceQuery = DbSet.Include(x => x.Role).AsQueryable();
        var data = await GetPagedCursor(sourceQuery, query);
        return new PagedResultModel<User>
        {
            Data = data,
            CountTotal = 100
        };
    }

    public async ValueTask<User?> GetUserById(int userId)
    {
        return await _cacheRepository.GetOrSetAsync<User>($"user-{userId}",
            async (old) => await DbSet.FindAsync(userId));
    }

    public void AddOrUpdateWorkerProfile(Worker worker)
    {
        if (worker.Id == 0)
        {
            _applicationDbContext.Set<Worker>().Add(worker);
            return;
        }

        _applicationDbContext.Update(worker);
    }
}
