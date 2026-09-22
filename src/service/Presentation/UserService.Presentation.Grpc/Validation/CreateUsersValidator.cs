using FluentValidation;
using UserService.Presentation.Grpc;

namespace Itmo.HighLoad.Presentation.Grpc.Validation;

public class CreateUsersValidator : AbstractValidator<CreateUsersRequest>
{
    public CreateUsersValidator()
    {
        RuleForEach(x => x.Entries)
            .SetValidator(new UserValidator());
    }
}