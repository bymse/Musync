namespace Musync.DataLayer.Queue.Models;

public class MusicPostModel : IPost
{
    public string Author { get; set; }
    public string Title { get; set; }
    
    public int FeedId { get; init; }
    public string PostExternalId { get; init; }
}