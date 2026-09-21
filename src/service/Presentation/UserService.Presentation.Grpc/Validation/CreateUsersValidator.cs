using FluentValidation;

namespace UserService.Presentation.Grpc.Validation;

public class CreateUsersValidator : AbstractValidator<CreateUsersRequest>
{
    public CreateUsersValidator()
    {
        RuleForEach(x => x.Entries)
            .SetValidator(new UserValidator());
    }
}