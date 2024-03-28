using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Homework
{
    public class WeaponView : Behaviour
    {
        [SerializeField] private Image _weaponImage;
        [SerializeField] private TextMeshProUGUI _ammoText;

        [SerializeField] private SpriteDatabase _spriteDatabase;

        private IReadableWeapon _currentWeapon;
        
        private IReadableWeaponManager _weaponManager;
        
        public void Construct(IReadableWeaponManager weaponManager)
        {
            weaponManager.WeaponChanged += OnWeaponChanged;
            
            _weaponManager = weaponManager;
        }

        private void SetAmmoText(int value)
        {
            _ammoText.text = value.ToString();
        }

        private void OnWeaponChanged(IReadableWeapon weapon)
        {
            if (_currentWeapon != null)
            {
                _currentWeapon.AmmoChanged -= OnCurrentWeaponAmmoChanged;
            }
            
            _currentWeapon = weapon;
            _currentWeapon.AmmoChanged += OnCurrentWeaponAmmoChanged;

            SetAmmoText(_currentWeapon.Ammo);
            
            if (!_spriteDatabase.TryGetValue(weapon.Name, out var sprite))
            {
                throw new NullReferenceException("Отсутствует спрайт оружия");
            }

            _weaponImage.sprite = sprite;
            _weaponImage.preserveAspect = true;
        }

        private void OnCurrentWeaponAmmoChanged(int ammo)
        {
            SetAmmoText(ammo);
        }

        public void Clear()
        {
            _weaponManager.WeaponChanged -= OnWeaponChanged;
        }
    }
}