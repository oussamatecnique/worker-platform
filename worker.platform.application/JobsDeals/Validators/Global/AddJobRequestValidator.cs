using FluentValidation;

namespace worker.platform.application.JobsDeals.Validators.Global;

public class AddJobRequestValidator: AbstractValidator<AddJobRequestDto>
{
    public AddJobRequestValidator()
    {
        RuleFor(x => x.TaskTypeId).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Details).NotEmpty();
        RuleFor(x => x.StartDate).GreaterThanOrEqualTo(DateTime.Now);
    }
}
