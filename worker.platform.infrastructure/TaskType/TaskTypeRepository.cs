using worker.platform.application.BackOffice.Repositories;
using worker.platform.infrastructure.Common;

namespace worker.platform.infrastructure.TaskType;

public class TaskTypeRepository(ApplicationDbContext applicationDbContext) : RepositoryBase<domain.Entities.TaskType, int>(applicationDbContext), ITaskTypeRepository
{
}
