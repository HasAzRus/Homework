using System;
using System.Collections.Generic;
using UnityEngine;

namespace Homework
{
    public class PlayerDataSaveLoad : Behaviour
    {
        [SerializeField] private string _healthKey;
        [SerializeField] private string _currentWeaponIndexKey;
        [SerializeField] private string _ammoKey;
        [SerializeField] private string _itemsKey;
        
        public PlayerData Load(IReadableData loadData)
        {
            var names = loadData.Read(_itemsKey + "_Names", Array.Empty<string>());
            var counts = loadData.Read(_itemsKey + "_Counts", Array.Empty<int>());

            var items = new List<Item>();

            for (var i = 0; i < names.Length; i++)
            {
                items.Add(new Item(names[i], counts[i]));
            }

            var playerData = new PlayerData
            {
                Health = loadData.Read(_healthKey, 100f),
                CurrentWeaponIndex = loadData.Read(_currentWeaponIndexKey, -1),
                Ammo = loadData.Read(_ammoKey, 0),
                Items = items.ToArray()
            };

            return playerData;
        }

        public void Save(IWritableData saveData, PlayerData data)
        {
            var names = new string[data.Items.Length];
            var counts = new int[data.Items.Length];

            for (var i = 0; i < names.Length; i++)
            {
                names[i] = data.Items[i].Name;
                counts[i] = data.Items[i].Count;
            }
            
            saveData.Write(_healthKey, data.Health);
            saveData.Write(_currentWeaponIndexKey, data.CurrentWeaponIndex);
            saveData.Write(_ammoKey, data.Ammo);
            saveData.Write(_itemsKey + "_Names", names);
            saveData.Write(_itemsKey + "_Counts", counts);
        }
    }
}