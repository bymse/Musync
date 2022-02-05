using Musync.DataLayer.Durable.Entity;

namespace Musync.Reader.Models;

public interface IFeedReader
{
    Task ReadAsync(Feed feed);
}