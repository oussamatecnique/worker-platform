using FluentValidation;
using worker.platform.application.Users.DTOs;

namespace worker.platform.application.Users.Validators;

public class SigninDtoValidator: AbstractValidator<SignInUserDto>
{
    public SigninDtoValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}
