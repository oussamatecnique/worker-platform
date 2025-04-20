namespace worker.platform.domain.ValueObjects;

public class JobRequestParams
{
    public string Description { get; set; }

    public List<JobRequestParamsValues> JobRequestParamsValues { get; set; }
}
