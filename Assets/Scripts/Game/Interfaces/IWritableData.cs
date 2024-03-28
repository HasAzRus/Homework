namespace Homework
{
    public interface IWritableData
    {
        void Write<T>(string key, T value);
    }
}