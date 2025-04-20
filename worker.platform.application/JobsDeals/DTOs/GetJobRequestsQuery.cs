using worker.platform.application.Common.Models;

namespace worker.platform.application.JobsDeals.DTOs;

public record GetJobRequestsQuery(int WorkerId, DateTime From, DateTime To, int PageNumber, int PageSize)
    : GetPagedQuery(PageNumber, PageSize, string.Empty, string.Empty);
