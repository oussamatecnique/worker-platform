using worker.platform.domain.ValueObjects;

namespace worker.platform.application.BackOffice.DTOs;

public record AddTaskTypeParamsDefinitionDto(AttributeDefinitionDto[] AttributeDefinitionDtos, int taskTypeId);

public record AttributeDefinitionDto(string AttributeName, ParamAttributeType AttributeType);
