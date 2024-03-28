namespace Homework
{
    public interface IReadableData
    {
        T Read<T>(string key, T valueByDefault);
    }
}