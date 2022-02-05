using Musync.Reader;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => { services.AddHostedService<FeedReadingWorker>(); })
    .Build();

await host.RunAsync();