using System.Security.AccessControl;
using Validot;
using worker.platform.application.Users.DTOs;

namespace worker.platform.application.Users.Validators;

public class SignupUserValidator2: ISpecificationHolder<SignUpUserDto>
{
    public Specification<SignUpUserDto> Specification { get; }

    public SignupUserValidator2()
    {
        Specification<string> emailSpecification = s => s
            .Email();

        Specification<SignUpUserDto> specification = s => s.Member(x => x.Email, emailSpecification)
            .Member(x => x.Password, x => x.NotEmpty())
            .Member(x => x.RoleId, x => x.Rule(value => Role.Constants.AllRoles.Contains(value)));

        Specification = specification;
    }
}
