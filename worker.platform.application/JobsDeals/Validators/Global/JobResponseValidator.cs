using FluentValidation;

namespace worker.platform.application.JobsDeals.Validators.Global;

public class JobResponseValidator : AbstractValidator<JobResponseDto>
{
    public JobResponseValidator()
    {
        RuleFor(x => x.WorkerId).GreaterThan(0);
        RuleFor(x => x.JobRequestId).GreaterThan(0);
    }
}
