namespace worker.platform.domain.Entities;

public class JobAssignment: BaseEntity
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public Customer Customer { get; set; }

    public int AssigneeId { get; set; }

    public Worker Assignee { get; set; }

    public int JobCategoryId { get; set; }

    public JobCategory JobCategory { get; set; }

    public string Description { get; set; }

    public DateTime StartTime { get; set; }

    public decimal Price { get; set; }

    public ServiceJobStatus Status { get; set; }
}

public enum ServiceJobStatus
{
    Pending,
    Approved,
    Rejected,
    Cancelled,
    Done
}
