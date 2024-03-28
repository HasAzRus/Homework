using System;

namespace Homework
{
    public class Slot : ISlot
    {
        public string Name { get; private set; }
        public int Count { get; private set; }
        public int Index { get; }
        
        public bool IsAssigned { get; private set; }

        public Slot(int index)
        {
            Index = index;
            IsAssigned = false;
        }
        
        private void Clear()
        {
            Name = string.Empty;
            IsAssigned = false;
        }

        public void Assign(string name)
        {
            Name = name;
            IsAssigned = true;
        }

        public void Add(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("Значение amount ниже нуля");
            }
            
            Count += amount;
        }

        public void Remove(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("Значение amount ниже нуля");
            }

            var value = Count - amount;

            if (value > 0)
            {
                Count -= value;
            }
            else
            {
                Count = 0;
                
                Clear();
            }
        }

        public void Remove()
        {
            Count = 0;
            Clear();
        }
    }
}