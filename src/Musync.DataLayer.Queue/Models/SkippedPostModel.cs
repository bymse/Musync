namespace Musync.DataLayer.Queue.Models;

public class SkippedPostModel : IPost
{
    public SkippedPostModel(int feedId, string postExternalId)
    {
        FeedId = feedId;
        PostExternalId = postExternalId;
    }

    public int FeedId { get; }
    public string PostExternalId { get; }
}