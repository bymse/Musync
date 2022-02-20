using Microsoft.Extensions.Configuration;

namespace Musync.Common;

public class Config : IConfig
{
    private readonly IConfiguration configuration;

    public Config(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public T GetValue<T>(string key) => configuration.GetValue<T>(key);
}