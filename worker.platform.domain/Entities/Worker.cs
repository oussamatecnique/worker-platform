namespace worker.platform.domain.Entities;

public class Worker: BaseEntity
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string PhoneNumber { get; set; }

    public string Address { get; set; }

    public string CIN { get; set; }

    public int JobCategoryId { get; set; }

    public JobCategory JobCategory { get; set; }

    public int CityId { get; set; }

    public City City { get; set; }

    public int UserId { get; set; }

    public User User { get; set; }

    public ICollection<JobAssignment> JobAssignments { get; set; }
}
