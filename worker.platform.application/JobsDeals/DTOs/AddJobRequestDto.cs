namespace worker.platform.application.JobsDeals.DTOs;

public record AddJobRequestDto(int TaskTypeId, string Details, int CustomerId, DateTime StartDate, int CityId, JobRequestParamsDto Params);
