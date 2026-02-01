using FluentValidation;
using worker.platform.application.Users.DTOs;

namespace worker.platform.application.Users.Validators;

public class SignUpUserDtoValidator: AbstractValidator<SignUpUserDto>
{
    public SignUpUserDtoValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.Password).MinimumLength(6);
        //roles start from index 1
        RuleFor(x => x.RoleId).GreaterThan(0);
        
    }
    
}