using FluentValidation;
using worker.platform.application.BackOffice.DTOs;

namespace worker.platform.application.BackOffice.Validators.Global;

public class AddTaskTypeValidator: AbstractValidator<AddTaskTypeDto>
{
    public AddTaskTypeValidator()
    {
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.jobCategoryId).GreaterThanOrEqualTo(0);
    }
}
