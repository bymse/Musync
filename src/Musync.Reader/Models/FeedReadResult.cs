using Musync.DataLayer.Queue.Models;

namespace Musync.Reader.Models;

public class FeedReadResult
{
    public static readonly FeedReadResult Null = new();
    
    public IReadOnlyList<MusicPostModel> MusicPostModels { get; init; } = null!;
    public IReadOnlyList<SkippedPostModel> SkippedPostModels { get; init; } = null!;
    public string LastPostId { get; init; } = null!;
}