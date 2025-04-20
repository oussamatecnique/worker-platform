namespace worker.platform.domain.Entities;

public class JobCategory: BaseEntity
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
}
