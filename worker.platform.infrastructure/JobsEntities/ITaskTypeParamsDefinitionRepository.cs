using worker.platform.application.JobsDeals.Repositories;
using worker.platform.domain.Entities;
using worker.platform.infrastructure.Common;

namespace worker.platform.infrastructure.JobsEntities;

public class ITaskTypeParamsDefinitionRepository(ApplicationDbContext applicationDbContext) : RepositoryBase<TaskTypeParamsDefinition, int>(applicationDbContext), ITaskTypeParamDefinitionRepository
{

    public IEnumerable<string> GetAttributesByCategoryId(int categoryId) =>
        DbSet.Where(x => x.TaskTypeId.Equals(categoryId)).Select(x => x.AttributeName);
}
