using Musync.DataLayer.Durable.Entity;

namespace Musync.Reader.Models;

public class FeedReaderFactory : IFeedReaderFactory
{
    public IFeedReader GetReader(FeedType type)
    {
        return type switch
        {
            FeedType.VkWall => throw new Exception(),
            _ => throw new InvalidOperationException($"Reader for FeedType.{type} doesn't specified")
        };
    }
}