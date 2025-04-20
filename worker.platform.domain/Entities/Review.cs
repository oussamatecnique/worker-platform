namespace worker.platform.domain.Entities;

public class Review: BaseEntity
{
    public int Id { get; set; }

    public int Rating { get; set; }

    public string Comment { get; set; }

    public Worker Worker { get; set; }

    public int WorkerId { get; set; }

    public int CustomerId { get; set; }

    public Customer Customer { get; set; }

    public int JobAssignmentId { get; set; }

    public JobAssignment JobAssignment { get; set; }
}
