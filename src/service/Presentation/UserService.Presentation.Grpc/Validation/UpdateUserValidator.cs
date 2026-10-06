using FluentValidation;

namespace UserService.Presentation.Grpc.Validation;

public class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Invalid Id");

        When(x => x.HasPassword, () =>
        {
            RuleFor(x => x.Password)
                .MinimumLength(7).WithMessage("Password must be longer than 6 characters");
        });

        When(x => x.BirthDate is not null, () =>
        {
            RuleFor(x => x.BirthDate)
                .SetValidator(new DateValidator());
        });
    }
}