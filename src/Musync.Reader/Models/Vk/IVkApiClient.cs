using VkNet.Model;

namespace Musync.Reader.Models.Vk;

public interface IVkApiClient
{
    Task<WallGetObject> GetWallAsync(string feedId);
}