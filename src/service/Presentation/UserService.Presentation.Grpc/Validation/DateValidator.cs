using FluentValidation;

namespace UserService.Presentation.Grpc.Validation;

public class DateValidator : AbstractValidator<Date>
{
    public DateValidator()
    {
        RuleFor(x => x.Year)
            .LessThanOrEqualTo(DateTime.UtcNow.Year);

        RuleFor(x => x)
            .Must(IsValidDate).WithMessage("Invalid date");
    }

    private bool IsValidDate(Date date)
    {
        if (date.Year == 0 || date.Month == 0 || date.Day == 0)
        {
            return false;
        }

        return date.Month >= 1 && date.Month <= 12 && date.Day >= 1 &&
               date.Day <= DateTime.DaysInMonth(date.Year, date.Month);
    }
}