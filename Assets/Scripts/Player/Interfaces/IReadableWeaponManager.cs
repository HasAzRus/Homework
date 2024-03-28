using System;

namespace Homework
{
    public interface IReadableWeaponManager
    {
        event Action<IReadableWeapon> WeaponChanged;
    }
}