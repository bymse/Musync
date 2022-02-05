using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Musync.DataLayer.Durable.Entity;
using Musync.DataLayer.Durable.Repositories;
using Musync.Reader.Models;
using Musync.Utilities;
using NUnit.Framework;

namespace Musync.Tests.Reader;

public class FeedsProcessorTest
{
    private readonly DateTimeOffset expectedLastReadTime = new(2020, 1, 1, 1, 1, 1, TimeSpan.Zero);

    [Test]
    public void ShouldUpdateLastRead_OnReadEnd()
    {
        var feed = new Feed();

        GetProcessor(feed, new SilentReader())
            .ProcessAsync(CancellationToken.None)
            .Wait();
        
        feed.LastReadTime.Should().Be(expectedLastReadTime);
    }

    [Test]
    public void Should_Not_UpdateLastReadTime_OnException()
    {
        var initialDateTime = DateTimeOffset.Now;
        var feed = new Feed()
        {
            LastReadTime = initialDateTime
        };

        GetProcessor(feed, new ReaderWithException())
            .ProcessAsync(CancellationToken.None)
            .Wait();

        feed.LastReadTime.Should().Be(initialDateTime);
    }

    private FeedsProcessor GetProcessor(Feed feed, IFeedReader reader)
    {
        var config = new Mock<IConfig>();
        config.Setup(r => r.GetValue<TimeSpan>("Reader:PollingInterval")).Returns(TimeSpan.Zero);

        var repositoryMock = new Mock<IFeedRepository>();
        repositoryMock
            .SetupSequence(r => r.GetFeeds(It.IsAny<DateTimeOffset>(), It.IsAny<int>()))
            .Returns(new[] { feed })
            .Returns(Array.Empty<Feed>())
            ;
        var factoryMock = new Mock<IFeedReaderFactory>();
        factoryMock
            .Setup(r => r.GetReader(It.IsAny<FeedType>()))
            .Returns(reader);

        var dateTimeManager = new Mock<IDateTimeManager>();
        dateTimeManager
            .SetupGet(e => e.Now)
            .Returns(expectedLastReadTime);

        return new FeedsProcessor(
            new Mock<ILogger<FeedsProcessor>>().Object,
            repositoryMock.Object,
            factoryMock.Object,
            dateTimeManager.Object,
            config.Object
        );
    }

    public class SilentReader : IFeedReader
    {
        public Task ReadAsync(Feed feed)
        {
            return Task.CompletedTask;
        }
    }

    public class ReaderWithException : IFeedReader
    {
        public Task ReadAsync(Feed feed)
        {
            throw new System.NotImplementedException();
        }
    }
}