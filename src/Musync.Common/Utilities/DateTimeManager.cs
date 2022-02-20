namespace Musync.Common;

public class DateTimeManager : IDateTimeManager
{
    public DateTimeOffset Now => DateTimeOffset.Now;
}