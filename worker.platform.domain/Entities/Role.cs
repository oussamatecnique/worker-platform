namespace worker.platform.domain.Entities;

public class Role
{
    public int Id { get; set; }

    public string Name { get; set; }

    public class Constants
    {
        public const int AdminKey = 1;
        public const int WorkerKey = 2;
        public const int CustomerKey = 3;

        public static readonly int[] AllRoles = [AdminKey, WorkerKey, CustomerKey];
    }
}
