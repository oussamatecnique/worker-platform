using Validot;
using worker.platform.application.JobsDeals.Repositories;

namespace worker.platform.application.JobsDeals.Validators;

public class AddJobRequestValidator: ISpecificationHolder<AddJobRequestDto>
{
    public Specification<AddJobRequestDto> Specification { get; }

    public AddJobRequestValidator(ICacheTaskTypeParamsDefinitionRepository taskTypeParamsDefinitionRepository)
    {
        Specification<AddJobRequestDto> specification = s =>
            s.Member(x => x.TaskTypeId, x => x.Positive())
                .Member(x => x.Details, x => x.NotEmpty())
                .Member(x => x.StartDate, x => x.After(DateTime.Now));
    }
}
