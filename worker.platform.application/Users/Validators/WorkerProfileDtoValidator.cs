using FluentValidation;
using worker.platform.application.Users.DTOs;

namespace worker.platform.application.Users.Validators;

public class WorkerProfileDtoValidator : AbstractValidator<WorkerProfileDto>
{
    public WorkerProfileDtoValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MinimumLength(1).MaximumLength(100);
        RuleFor(x => x.LastName).NotEmpty().MinimumLength(1).MaximumLength(100);
        RuleFor(x => x.Address).NotEmpty().MinimumLength(1).MaximumLength(500);
        RuleFor(x => x.CIN).NotEmpty().Length(8);
        RuleFor(x => x.PhoneNumber).NotEmpty();
        RuleFor(x => x.JobCategoryId).NotEmpty().GreaterThanOrEqualTo(0);
    }
}
