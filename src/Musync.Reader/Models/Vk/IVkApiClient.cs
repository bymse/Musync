using VkNet.Model;

namespace Musync.Reader.Models.Vk;

public interface IVkApiClient : IDisposable
{
    Task<WallGetObject> GetWallAsync(string feedId);
    Task EnsureAuthorizedAsync();
}