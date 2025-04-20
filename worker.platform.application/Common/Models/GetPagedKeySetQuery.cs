using System.Text.Json;

namespace worker.platform.application.Common.Models;

public class GetPagedKeySetQuery
{
    public string SortField { get; set; }

    public SortDirection SortDirection { get; set; }

    public int PageSize { get; set; }

    public JsonElement LastKeyValue { get; set; }
}

public enum SortDirection
{
    Ascending,
    Descending
}
