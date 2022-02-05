using Musync.DataLayer.Durable.Entity;
using Musync.DataLayer.Durable.Repositories;

namespace Musync.Reader.Models;

public class FeedsProcessor
{
    private readonly ILogger<FeedsProcessor> logger;
    private readonly FeedReaderFactory feedReaderFactory;
    private readonly IFeedRepository feedRepository;
    private readonly IConfiguration configuration;

    public FeedsProcessor(
        ILogger<FeedsProcessor> logger,
        IFeedRepository feedRepository,
        IConfiguration configuration,
        FeedReaderFactory feedReaderFactory
    )
    {
        this.logger = logger;
        this.feedRepository = feedRepository;
        this.configuration = configuration;
        this.feedReaderFactory = feedReaderFactory;
    }

    public async Task ProcessAsync(CancellationToken stoppingToken)
    {
        const int maxFeedsCount = 3;

        while (!stoppingToken.IsCancellationRequested)
        {
            var pollingInterval = configuration.GetValue<TimeSpan>("Reader:PollingInterval");
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
            feed.LastReadTime = DateTimeOffset.Now;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to read Feed {id} of type {type}", feed.FeedId, feed.FeedType);
        }
    }
}