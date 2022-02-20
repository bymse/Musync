namespace Musync.Common.Utilities;

public class DateTimeManager : IDateTimeManager
{
    public DateTimeOffset Now => DateTimeOffset.Now;
}