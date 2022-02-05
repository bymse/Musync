namespace Musync.Utilities;

public class DateTimeManager : IDateTimeManager
{
    public DateTimeOffset Now => DateTimeOffset.Now;
}