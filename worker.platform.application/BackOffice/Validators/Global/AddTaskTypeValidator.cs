using Validot;
using worker.platform.application.BackOffice.DTOs;

namespace worker.platform.application.BackOffice.Validators.Global;

public class AddTaskTypeValidator: ISpecificationHolder<AddTaskTypeDto>
{
    public Specification<AddTaskTypeDto> Specification { get; }

    public AddTaskTypeValidator()
    {
        Specification<AddTaskTypeDto> specification = s => s.Member(x => x.Description, x => x.NotEmpty())
            .Member(x => x.jobCategoryId, x => x.GreaterThan(-1));

        Specification = specification;
    }
}
