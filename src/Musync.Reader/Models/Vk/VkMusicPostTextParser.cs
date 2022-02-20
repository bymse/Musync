using System.Text.RegularExpressions;
using Musync.Common.Utilities.Extensions;

namespace Musync.Reader.Models.Vk;

public record MusicAlbum(string Author, string Title);

public static class VkMusicPostTextParser
{
    private static readonly Regex AuthorTitleRegex = new(@"(?<author>.*)[-—](?<title>.*)(?<year>\(.*\))");
    
    public static MusicAlbum? ParseToAlbumInfo(string text)
    {
        var firstLine = text.ReplaceLineEndings()
            .Split(Environment.NewLine)
            .FirstOrDefault();

        if (firstLine.IsNullOrEmpty())
        {
            return null;
        }

        var match = AuthorTitleRegex.Match(firstLine);
        if (!match.Success)
        {
            return null;
        }

        return new MusicAlbum(match.Groups["author"].Value.Trim(), match.Groups["title"].Value.Trim());
    }
}