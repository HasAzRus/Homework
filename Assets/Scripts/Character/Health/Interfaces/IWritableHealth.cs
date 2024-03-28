namespace Homework
{
    public interface IWritableHealth
    {
        void Apply(float value);
        void Add(float amount);
        void Remove(float amount);
    }
}