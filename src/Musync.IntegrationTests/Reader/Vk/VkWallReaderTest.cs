using System;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using Moq;
using Musync.Common.Utilities;
using Musync.DataLayer.Durable.Entity;
using Musync.DataLayer.Queue.Models;
using Musync.Reader.Models.Vk;
using NUnit.Framework;
using VkNet;

namespace Musync.IntegrationTests.Reader.Vk;

[TestFixture, Parallelizable(ParallelScope.None)]
public class VkWallReaderTest
{
    private IVkApiClient vkApiClient = null!;

    [SetUp]
    public async Task SetUp()
    {
        vkApiClient = GetClient();
        await vkApiClient.EnsureAuthorizedAsync();
    }

    [Test]
    public async Task TestFromPredefinedCommunity()
    {
        var reader = new VkWallReader(vkApiClient);
        var feed = new Feed
        {
            // https://vk.com/musync_integration_tests
            ExternalFeedId = "-210822923",
            LastPostId = null,
            FeedId = 1
        };

        var result = await reader.ReadAsync(feed);

        using (new AssertionScope())
        {
            result.LastPostId.Should().Be(3);
            result.SkippedPostModels.Should().BeEquivalentTo(new SkippedPostModel[]
            {
                new(feed.FeedId, "3")
            });
            result.MusicPostModels.Should().BeEquivalentTo(new MusicPostModel[]
            {
                new()
                {
                    Author = "Тестовый автор 1",
                    Title = "тестовое название 1",
                    FeedId = feed.FeedId,
                    PostExternalId = "1"
                },
                new()
                {
                    Author = "Тестовый автор 2",
                    Title = "тестовое название 2",
                    FeedId = feed.FeedId,
                    PostExternalId = "2"
                }
            });
        }
    }


    [TearDown]
    public void TearDown()
    {
        vkApiClient.Dispose();
    }

    private static IVkApiClient GetClient()
    {
        var configMock = new Mock<IConfig>();
        configMock
            .Setup(r => r.GetValue<string>("Reader:Vk:AccessToken"))
            .Returns(Environment.GetEnvironmentVariable("MUSYNC_TESTS_VK_API_TOKEN")!);

        return new VkApiClient(new VkApi(), configMock.Object);
    }
}