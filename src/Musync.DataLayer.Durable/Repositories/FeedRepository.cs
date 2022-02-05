using Musync.DataLayer.Durable.Database;
using Musync.DataLayer.Durable.Entity;

namespace Musync.DataLayer.Durable.Repositories;

internal class FeedRepository : IFeedRepository
{
    private readonly MusyncDbContext dbContext;

    public FeedRepository(MusyncDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public IReadOnlyList<Feed> GetFeeds(DateTimeOffset maxLastRead, int maxCount)
    {
        return dbContext.Feeds
            .Where(e => e.LastReadTime < maxLastRead)
            .Take(maxCount)
            .ToArray();
    }

    public void SaveChanges()
    {
        dbContext.SaveChanges();
    }
}