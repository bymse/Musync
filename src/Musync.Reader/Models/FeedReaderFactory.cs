using Musync.DataLayer.Durable.Entity;

namespace Musync.Reader.Models;

public class FeedReaderFactory
{
    public IFeedReader GetReader(FeedType type)
    {
        return type switch
        {
            FeedType.VkWall => ,
            _ => throw new InvalidOperationException($"Reader for FeedType.{type} doesn't specified")
        };
    }
}