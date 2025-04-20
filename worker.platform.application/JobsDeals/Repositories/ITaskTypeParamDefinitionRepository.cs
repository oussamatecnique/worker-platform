using worker.platform.application.Common;

namespace worker.platform.application.JobsDeals.Repositories;

public interface ITaskTypeParamDefinitionRepository: IRepositoryBase<TaskTypeParamsDefinition, int>
{
    public IEnumerable<string> GetAttributesByCategoryId(int categoryId);
}
