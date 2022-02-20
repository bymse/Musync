namespace Musync.DataLayer.Queue.Models;

public interface IPost
{
    int FeedId { get; }
    string PostExternalId { get; }
}