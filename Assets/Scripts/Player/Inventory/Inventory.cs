using System;
using System.Collections.Generic;
using UnityEngine;

namespace Homework
{
    public class Inventory : IReadableInventory
    {
        public event Action<ISlot> Placed;
        public event Action<ISlot> Removed;
        
        private readonly Slot[] _slots;
        
        public Inventory(int capacity)
        {
            _slots = new Slot[capacity];

            for (var i = 0; i < capacity; i++)
            {
                _slots[i] = new Slot(i);
            }
        }

        public bool Place(string name, int count)
        {
            if (name == string.Empty)
            {
                throw new ArgumentNullException("Отсутствует значение name");
            }
            
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("Значение count ниже нуля");
            }
            
            Slot firstFreeSlot = null;
            
            foreach (var slot in _slots)
            {
                if (slot.IsAssigned)
                {
                    if (slot.Name == name)
                    {
                        slot.Add(count);
                        Placed?.Invoke(slot);
                        
                        Debug.Log($"Предмет {name} был добавлен в кол-ве {count}");

                        return true;
                    }
                }
                else
                {
                    if (firstFreeSlot == null)
                    {
                        firstFreeSlot = slot;
                    }
                }
            }

            if (firstFreeSlot == null)
            {
                return false;
            }
            
            firstFreeSlot.Assign(name);
            firstFreeSlot.Add(count);
            
            Placed?.Invoke(firstFreeSlot);
            Debug.Log($"Предмет {name} был добавлен в кол-ве {count}");

            return true;
        }

        public bool Remove(string name, int amount)
        {
            if (name == string.Empty)
            {
                throw new ArgumentNullException("Отсутствует значение name");
            }
            
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("Значение amount ниже нуля");
            }
            
            foreach (var slot in _slots)
            {
                if (!slot.IsAssigned)
                {
                    continue;
                }

                if (slot.Name != name)
                {
                    continue;
                }
                
                slot.Remove(amount);
                
                Removed?.Invoke(slot);
                Debug.Log($"Предмет {name} был удален из инвентаря в кол-ве {amount}");

                return true;
            }

            return false;
        }

        public bool Remove(string name)
        {
            if (name == string.Empty)
            {
                throw new ArgumentNullException("Отсутствует значение name");
            }

            foreach (var slot in _slots)
            {
                if (!slot.IsAssigned)
                {
                    continue;
                }

                if (slot.Name != name)
                {
                    continue;
                }
                
                slot.Remove();
                Removed?.Invoke(slot);
                
                Debug.Log($"Предмет {name} был удален из инвентаря");

                return true;
            }

            return false;
        }
        
        public Item[] GetItems()
        {
            var result = new List<Item>();

            foreach (var slot in _slots)
            {
                if (!slot.IsAssigned)
                {
                    continue;
                }
                
                result.Add(new Item(slot.Name, slot.Count));
            }

            return result.ToArray();
        }
    }
}