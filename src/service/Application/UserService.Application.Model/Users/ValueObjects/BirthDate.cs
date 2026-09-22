namespace Itmo.HighLoad.Application.Model.Users.ValueObjects;

public class BirthDate
{
    public BirthDate(DateOnly? value)
    {
        if (value > DateOnly.FromDateTime(DateTime.UtcNow))
        {
            throw new ArgumentException("Incorrect date");
        }

        Value = value;
    }

    public DateOnly? Value { get; private set; }
}