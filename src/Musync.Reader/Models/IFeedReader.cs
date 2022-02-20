using Musync.DataLayer.Durable.Abstract;

namespace Musync.Reader.Models;

public interface IFeedReader
{
    Task<FeedReadResult> ReadAsync(IFeed feed);
}