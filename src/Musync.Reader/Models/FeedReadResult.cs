using Musync.DataLayer.Queue.Models;

namespace Musync.Reader.Models;

public class FeedReadResult
{
    public IReadOnlyList<MusicPostModel> MusicPostModels { get; init; }
    public IReadOnlyList<SkippedPostModel> SkippedPostModels { get; init; }
    public long? LastPostId { get; init; }
}