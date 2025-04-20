namespace worker.platform.application.JobsDeals.Repositories;

public interface ICacheTaskTypeParamsDefinitionRepository
{
    public ValueTask<IEnumerable<string>> GetCacheAttributesByTaskTypeId(int categoryId);
}
