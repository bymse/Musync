using Musync.DataLayer.Durable.Entity;
using Musync.DataLayer.Durable.Repositories;
using Musync.Utilities;

namespace Musync.Reader.Models;

public class FeedsProcessor
{
    private readonly ILogger<FeedsProcessor> logger;
    private readonly IFeedReaderFactory feedReaderFactory;
    private readonly IFeedRepository feedRepository;
    private readonly IConfig config;
    private readonly IDateTimeManager dateTimeManager;

    public FeedsProcessor(
        ILogger<FeedsProcessor> logger,
        IFeedRepository feedRepository,
        IFeedReaderFactory feedReaderFactory, IDateTimeManager dateTimeManager, IConfig config)
    {
        this.logger = logger;
        this.feedRepository = feedRepository;
        this.feedReaderFactory = feedReaderFactory;
        this.dateTimeManager = dateTimeManager;
        this.config = config;
    }

    public async Task ProcessAsync(CancellationToken stoppingToken)
    {
        const int maxFeedsCount = 3;

        while (!stoppingToken.IsCancellationRequested)
        {
            var pollingInterval = config.GetValue<TimeSpan>("Reader:PollingInterval");
            var maxLastRead = DateTimeOffset.Now - pollingInterval;
            var feeds = feedRepository.GetFeeds(maxLastRead, maxFeedsCount);
            if (feeds.Count == 0)
            {
                break;
            }

            await Task.WhenAll(feeds.Select(ProcessFeedAsync));
            feedRepository.SaveChanges();
        }
    }

    private async Task ProcessFeedAsync(Feed feed)
    {
        try
        {
            await feedReaderFactory.GetReader(feed.FeedType).ReadAsync(feed);
            feed.LastReadTime = dateTimeManager.Now;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to read Feed {id} of type {type}", feed.FeedId, feed.FeedType);
        }
    }
}