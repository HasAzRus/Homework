using System;
using UnityEngine;

namespace Homework
{
    public sealed class PlayerWeaponManager : Behaviour, IReadableWeaponManager
    {
        public event Action<IReadableWeapon> WeaponChanged;
        
        [SerializeField] private Weapon[] _weapons;

        private IInteractable _interactable;

        public Weapon CurrentWeapon { get; private set; }
        public int CurrentWeaponIndex { get; private set; }

        public void Construct(GameObject owner, IInteractable interactable)
        {
            _interactable = interactable;

            foreach (var weapon in _weapons)
            {
                weapon.Construct(owner);
            }

            CurrentWeaponIndex = -1;
        }

        private void OnCurrentWeaponAmmoChanged(int ammo)
        {
            if (ammo > 0)
            {
                return;
            }

            if (!_interactable.RemoveItem(CurrentWeapon.AmmoName, 1))
            {
                return;
            }

            CurrentWeapon.Reload(CurrentWeapon.MaxAmmo);
        }

        public bool ChangeWeapon(int index)
        {
            var weapon = _weapons[index];

            if (CurrentWeapon == weapon)
            {
                Debug.Log("Оружие уже выбрано");
                
                return false;
            }
            
            if (!weapon.IsActive)
            {
                Debug.Log("Оружие недоступно");
                
                return false;
            }

            if (CurrentWeapon != null)
            {
                CurrentWeapon.AmmoChanged -= OnCurrentWeaponAmmoChanged;
            }

            weapon.AmmoChanged += OnCurrentWeaponAmmoChanged;

            CurrentWeapon = weapon;
            CurrentWeaponIndex = index;
            
            WeaponChanged?.Invoke(weapon);

            return true;
        }
    }
}