using Musync.DataLayer.Durable.Entity;

namespace Musync.DataLayer.Durable.Repositories;

public interface IFeedRepository
{
    IReadOnlyList<Feed> GetFeeds(DateTimeOffset maxLastRead, int maxCount);
    void SaveChanges();
}