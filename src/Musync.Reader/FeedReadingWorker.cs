using Musync.Reader.Models;

namespace Musync.Reader;

public class FeedReadingWorker : BackgroundService
{
    private readonly ILogger<FeedReadingWorker> logger;
    private readonly FeedsProcessor feedsProcessor;
    private readonly IConfiguration configuration;

    public FeedReadingWorker(
        ILogger<FeedReadingWorker> logger,
        FeedsProcessor feedsProcessor, IConfiguration configuration)
    {
        this.logger = logger;
        this.feedsProcessor = feedsProcessor;
        this.configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Starting");
            await feedsProcessor.ProcessAsync(stoppingToken);
            logger.LogInformation("Stopping");
            
            var delay = configuration.GetValue<TimeSpan>("Reader:WorkerDelay");
            await Task.Delay(delay, stoppingToken);
        }
    }
}