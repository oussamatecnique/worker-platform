using worker.platform.domain.ValueObjects;

namespace worker.platform.domain.Entities;

public class TaskTypeParamsDefinition
{
    public int Id { get; set; }

    public string AttributeName { get; set; }

    public ParamAttributeType AttributeType { get; set; }

    public int TaskTypeId { get; set; }

    public TaskType TaskType { get; set; }
}
