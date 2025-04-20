using Validot;
using worker.platform.application.BackOffice.DTOs;

namespace worker.platform.application.BackOffice.Validators.Global;

public class AddJobParamsValidator: ISpecificationHolder<AddTaskTypeParamsDefinitionDto>
{
    public Specification<AddTaskTypeParamsDefinitionDto> Specification { get; }


    public AddJobParamsValidator()
    {
        Specification<AddTaskTypeParamsDefinitionDto> specification =
            s => s.Member(x => x.taskTypeId, x => x.Positive())
                .Member(x => x.AttributeDefinitionDtos, x => x.NotEmptyCollection());

        Specification = specification;
    }
}
