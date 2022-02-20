using Musync.Common;
using Musync.Common.Utilities;
using VkNet;
using VkNet.Enums.SafetyEnums;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace Musync.Reader.Models.Vk;

public class VkApiClient : IVkApiClient
{
    private readonly VkApi vkApi;
    private readonly IConfig config;

    public VkApiClient(VkApi vkApi, IConfig config)
    {
        this.vkApi = vkApi;
        this.config = config;
    }

    public async Task<WallGetObject> GetWallAsync(string feedId)
    {
        await EnsureAuthorizedAsync();
        var parameters = new WallGetParams
        {
            OwnerId = long.Parse(feedId),
            Filter = WallFilter.All,
            Count = 10,
            Extended = true
        };

        return await vkApi.Wall.GetAsync(parameters);
    }

    public async Task EnsureAuthorizedAsync()
    {
        if (!vkApi.IsAuthorized)
        {
            await vkApi.AuthorizeAsync(new ApiAuthParams()
            {
                AccessToken = config.GetValue<string>("Reader:Vk:AccessToken"),
            });
        }
    }

    public void Dispose()
    {
        vkApi.Dispose();
    }
}