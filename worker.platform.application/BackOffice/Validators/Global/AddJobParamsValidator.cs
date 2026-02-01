using FluentValidation;
using worker.platform.application.BackOffice.DTOs;

namespace worker.platform.application.BackOffice.Validators.Global;

public class AddJobParamsValidator: AbstractValidator<AddTaskTypeParamsDefinitionDto>
{
    public AddJobParamsValidator()
    {
        RuleFor(x => x.taskTypeId).GreaterThanOrEqualTo(0);
        RuleFor(x => x.AttributeDefinitionDtos).NotEmpty();
    }
}
