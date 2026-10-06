using FluentValidation;

namespace RateLimiter.Writer.Presentation.Validation;

public class UpdateLimitValidator : AbstractValidator<UpdateLimitRequest>
{
    public UpdateLimitValidator()
    {
        RuleFor(x => x.RequestsPerMinute)
            .GreaterThan(0)
            .WithMessage("Supports only positive RPM")
            .WithErrorCode("422");
    }
}