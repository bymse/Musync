using Musync.Common;
using Musync.DataLayer.Durable.Entity;

namespace Musync.Reader.Models;

public interface IFeedReaderFactory
{
    IFeedReader GetReader(FeedType type);
}