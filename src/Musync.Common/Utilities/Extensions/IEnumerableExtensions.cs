namespace Musync.Common.Utilities.Extensions;

// ReSharper disable once InconsistentNaming
public static class IEnumerableExtensions
{
    public static IEnumerable<T> If<T>(this IEnumerable<T> src, bool condition, Func<IEnumerable<T>, IEnumerable<T>> apply)
    {
        return condition ? apply(src) : src;
    }
}