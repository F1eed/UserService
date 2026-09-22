using FluentValidation;
using UserService.Presentation.Grpc;

namespace Itmo.HighLoad.Presentation.Grpc.Validation;

public class UserValidator : AbstractValidator<UserEntry>
{
    public UserValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty().WithMessage("Login must not be empty")
            .MinimumLength(3).WithMessage("Login must be longer than 2 characters");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password must not be empty")
            .MinimumLength(7).WithMessage("Password must be longer than 6 characters");

        RuleFor(x => x.BirthDate)
            .SetValidator(new DateValidator());
    }
}