using worker.platform.domain.ValueObjects;

namespace worker.platform.application.JobsDeals.DTOs;

public record JobResponseDto(int JobRequestId, int WorkerId, string Message, decimal SuggestedPrice, int EstimatedTime, EstimatedTimeUnit EstimatedTimeUnit, int CityId);
