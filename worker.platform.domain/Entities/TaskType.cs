namespace worker.platform.domain.Entities;

public class TaskType: BaseEntity
{
    public int Id { get; set; }

    public string Description { get; set; }

    public int JobCategoryId { get; set; }

    public JobCategory JobCategory { get; set; }

    public string Logo { get; set; }
}
