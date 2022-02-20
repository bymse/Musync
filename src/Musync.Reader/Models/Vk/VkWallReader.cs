using Musync.Common.Utilities.Extensions;
using Musync.DataLayer.Durable.Abstract;
using Musync.DataLayer.Queue.Models;
using VkNet.Model.Attachments;

namespace Musync.Reader.Models.Vk;

public class VkWallReader : IFeedReader
{
    private readonly IVkApiClient vkApiClient;

    public VkWallReader(IVkApiClient vkApiClient)
    {
        this.vkApiClient = vkApiClient;
    }

    public async Task<FeedReadResult?> ReadAsync(IFeed feed)
    {
        var wall = await vkApiClient.GetWallAsync(feed.ExternalFeedId);
        var lastPostId = feed.LastPostId.IsNullOrEmpty()
            ? (long?)null
            : long.Parse(feed.LastPostId);
        
        var posts = wall.WallPosts
            .If(lastPostId.HasValue, e => e.Where(r => r.Id > lastPostId))
            .Select(e => ToPostModel(e, feed))
            .ToArray();

        if (!posts.Any())
        {
            return null;
        }

        return new FeedReadResult
        {
            LastPostId = wall.WallPosts.First().Id.ToString()!,
            MusicPostModels = posts.OfType<MusicPostModel>().ToArray(),
            SkippedPostModels = posts.OfType<SkippedPostModel>().ToArray()
        };
    }

    private static IPost ToPostModel(Post post, IFeed feed)
    {
        if (!IsMusic(post))
        {
            return ToSkippedModel(post, feed);
        }

        var album = VkMusicPostTextParser.ParseToAlbumInfo(post.Text);
        if (album == null)
        {
            return ToSkippedModel(post, feed);
        }

        return new MusicPostModel
        {
            Author = album.Author,
            Title = album.Title,
            FeedId = feed.FeedId,
            PostExternalId = post.Id.ToString()!
        };
    }

    private static SkippedPostModel ToSkippedModel(Post post, IFeed feed)
        => new(feed.FeedId, post.Id.ToString()!);

    private static bool IsMusic(Post post)
    {
        var hasAudio = post.Attachments.Any(e => e.Type == typeof(Audio));
        if (hasAudio)
        {
            return true;
        }

        return post
            .Attachments
            .Where(e => e.Type == typeof(Link))
            .Select(e => (Link)e.Instance)
            .Any(e => e.Description == "Playlist");
    }
}