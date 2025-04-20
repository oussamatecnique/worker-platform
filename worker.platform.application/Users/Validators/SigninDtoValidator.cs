using Validot;
using worker.platform.application.Users.DTOs;

namespace worker.platform.application.Users.Validators;

public class SigninDtoValidator: ISpecificationHolder<SignInUserDto>
{
    public Specification<SignInUserDto> Specification { get; }

    public SigninDtoValidator()
    {
        Specification<string> emailSpecification = s => s
            .Email()
            .EndsWith("@gmail.com");

        Specification<SignInUserDto> signinUserDto = s => s
            .Member(m => m.Email, emailSpecification).WithMessage("Invalid email")
            .Member(m => m.Password, m => m.NotEmpty()).WithMessage("Password is required");

        Specification = signinUserDto;
    }
}
