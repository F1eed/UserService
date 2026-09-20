namespace UserService.Presentation.Grpc.Mapping;

public static class BirthDateMappingExtension
{
    public static Date MapToProto(this DateOnly date)
        => new Date
        {
            Year = date.Year,
            Day = date.Day,
            Month = date.Month,
        };
}