using Musync.DataLayer.Queue.Models;

namespace Musync.Reader.Models;

public class FeedReadResult
{
    public static readonly FeedReadResult Null = new();
    
    public IReadOnlyList<MusicPostModel> MusicPostModels { get; init; }
    public IReadOnlyList<SkippedPostModel> SkippedPostModels { get; init; }
    public string LastPostId { get; init; }
}