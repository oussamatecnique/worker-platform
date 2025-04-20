using worker.platform.domain.ValueObjects;

namespace worker.platform.domain.Entities;

public class JobResponse: BaseEntity
{
    public int Id { get; set; }

    public string Message { get; set; }

    public decimal SuggestedPrice { get; set; }

    public int EstimatedTime { get; set; }

    public EstimatedTimeUnit EstimatedTimeUnit { get; set; }

    public int JobRequestId { get; set; }

    public JobRequest JobRequest { get; set; }

    public int WorkerId { get; set; }

    public Worker Worker { get; set; }
}
