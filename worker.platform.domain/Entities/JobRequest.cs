using System.Text.Json.Serialization;
using worker.platform.domain.Converters;
using worker.platform.domain.ValueObjects;

namespace worker.platform.domain.Entities;

public class JobRequest: BaseEntity
{
    public int Id { get; set; }

    public int TaskTypeId { get; set; }

    public TaskType TaskType { get; set; }

    public string Details { get; set; }

    public DateTime StartDate { get; set; }

    public int CustomerId { get; set; }

    public Customer Customer { get; set; }

    public int CityId { get; set; }

    public City City { get; set; }

    public List<JobRequestParams> Params { get; set; }
}
