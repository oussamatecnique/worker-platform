namespace worker.platform.domain.Entities;

public class User: BaseEntity
{
    public int Id { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public int RoleId { get; set; }

    public Role Role { get; set; }

    public DateTime LastLogin { get; set; }
}
