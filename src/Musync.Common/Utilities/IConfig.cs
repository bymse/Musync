namespace Musync.Common;

public interface IConfig
{
    T GetValue<T>(string key);
}