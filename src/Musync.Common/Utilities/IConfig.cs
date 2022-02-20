namespace Musync.Common.Utilities;

public interface IConfig
{
    T GetValue<T>(string key);
}