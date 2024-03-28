namespace Homework
{
    public interface ISlot : IItem
    {
        int Index { get; }
        bool IsAssigned { get; }
    }
}