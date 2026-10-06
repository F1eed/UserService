using FluentValidation;

namespace RateLimiter.Writer.Presentation.Validation;

public class LimitValidator : AbstractValidator<RateLimit>
{
    public LimitValidator()
    {
        RuleFor(x => x.RequestsPerMinute)
            .GreaterThan(0)
            .WithMessage("Supports only positive RPM");
    }
}