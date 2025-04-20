namespace worker.platform.application.Common.Models;

public class PagedResultModel<T>
{
    public IEnumerable<T> Data { get; set; }

    public int CountTotal { get; set; }
}
