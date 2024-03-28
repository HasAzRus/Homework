using System;

namespace Homework
{
    public interface IReadableInventory
    {
        event Action<ISlot> Placed;
        event Action<ISlot> Removed;

        Item[] GetItems();
    }
}