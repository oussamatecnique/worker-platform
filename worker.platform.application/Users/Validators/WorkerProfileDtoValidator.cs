using Validot;
using worker.platform.application.Users.DTOs;

namespace worker.platform.application.Users.Validators;

public class WorkerProfileDtoValidator : ISpecificationHolder<WorkerProfileDto>
{
    public Specification<WorkerProfileDto> Specification { get; }

    public WorkerProfileDtoValidator()
    {
        Specification<WorkerProfileDto> workerProfileSpecification =
            s => s.Member(x => x.FirstName, x => x.NotEmpty().LengthBetween(1, 100))
                .Member(x => x.LastName, x => x.NotEmpty().LengthBetween(1, 100))
                .Member(x => x.Address, x => x.NotEmpty().LengthBetween(1, 200))
                .Member(x => x.CIN, x => x.NotEmpty().ExactLength(8))
                .Member(x => x.PhoneNumber, x => x.NotEmpty())
                .Member(x => x.JobCategoryId, x => x.Positive());

        Specification = workerProfileSpecification;
    }
}
