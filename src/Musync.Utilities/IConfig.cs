namespace Musync.Utilities;

public interface IConfig
{
    T GetValue<T>(string key);
}