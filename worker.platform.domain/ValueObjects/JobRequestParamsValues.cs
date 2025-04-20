namespace worker.platform.domain.ValueObjects;

public class JobRequestParamsValues
{
    public string AttributeName { get; set; }
    
    public string AttributeValue { get; set; }

    public ParamAttributeType Type { get; set; }
}
