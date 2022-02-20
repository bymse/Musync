using Musync.Common;
using Musync.DataLayer.Durable.Entity;

namespace Musync.DataLayer.Durable.Abstract;

public interface IFeed
{
    int FeedId { get; }
    FeedType FeedType { get; }
    string ExternalFeedId { get; }
    DateTimeOffset LastReadTime { get; }
    string? LastPostId { get; set; }
}