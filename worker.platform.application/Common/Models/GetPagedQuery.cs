namespace worker.platform.application.Common.Models;

public record GetPagedQuery(int PageNumber, int PageSize, string SortColumn, string SortDirection);
