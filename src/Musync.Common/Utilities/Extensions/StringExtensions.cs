using System.Diagnostics.CodeAnalysis;

namespace Musync.Common.Utilities.Extensions;

public static class StringExtensions
{
    public static bool IsNullOrEmpty([NotNullWhen(false)] this string? str) => string.IsNullOrEmpty(str);
}