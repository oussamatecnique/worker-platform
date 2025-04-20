using worker.platform.domain.ValueObjects;

namespace worker.platform.application.JobsDeals.DTOs;

public record JobRequestParamsDto(string Description, List<JobRequestParamsValuesDto> JobRequestParamsValues);

public record JobRequestParamsValuesDto(string AttributeName, string AttributeValue, ParamAttributeType Type);
