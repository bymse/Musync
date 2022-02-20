namespace Musync.DataLayer.Queue.Models;

public class MusicPostModel : IPost
{
    public string Author { get; set; } = null!;
    public string Title { get; set; } = null!;

    public int FeedId { get; init; }
    public string PostExternalId { get; init; } = null!;
}