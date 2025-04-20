using Validot;
using worker.platform.application.Users.Repositories;

namespace worker.platform.application.JobsDeals.Validators;

public class JobResponseValidator: ISpecificationHolder<JobResponseDto>
{
    public Specification<JobResponseDto> Specification { get; }


    public JobResponseValidator(ICacheWorkerRepository cacheWorkerRepository)
    {
        ArgumentNullException.ThrowIfNull(cacheWorkerRepository);

        Specification<JobResponseDto> jobResponseSpecification =
            s => s.Member(x => x.WorkerId, x => x.Positive())
                .Member(x => x.JobRequestId, x => x.Positive());


        Specification = jobResponseSpecification;
    }
}
