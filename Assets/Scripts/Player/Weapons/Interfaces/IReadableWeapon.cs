using System;
using UnityEngine;

namespace Homework
{
    public interface IReadableWeapon
    {
        event Action<int> AmmoChanged;
        
        string Name { get; }
        bool IsActive { get; }
        
        int MaxAmmo { get; }
        int Ammo { get; }
        
        float Damage { get; }
        GameObject Owner { get; }
    }
}