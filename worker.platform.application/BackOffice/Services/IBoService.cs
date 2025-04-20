using worker.platform.application.BackOffice.DTOs;
using worker.platform.application.Common.Models;

namespace worker.platform.application.BackOffice;

public interface IBoService
{
    public Task<int> CreateTaskParamsDefinitionAsync(AddTaskTypeParamsDefinitionDto addTaskTypeParamsDefinitionDto, CancellationToken cancellationToken = default);

    public Task<PagedResultModel<UserDto>> GetPagedUsersAsync(GetPagedUsersDto query,
        CancellationToken cancellationToken = default
    );

    public Task<bool> AddAdminUserAsync(AddAdminUser data, CancellationToken cancellationToken = default);

    public Task<bool> CreateTaskTypeAsync(AddTaskTypeDto addTaskTypeDto, CancellationToken cancellationToken);
}

