using FluentValidation;

namespace RateLimiter.Writer.Presentation.Validation;

public class CreateLimitValidator : AbstractValidator<CreateLimitRequest>
{
    public CreateLimitValidator()
    {
        RuleFor(x => x.RateLimit)
            .SetValidator(new LimitValidator());
    }
}