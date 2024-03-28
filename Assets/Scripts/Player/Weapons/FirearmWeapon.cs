using System;
using UnityEngine;

namespace Homework
{
    public enum FirearmWeaponType
    {
        Single,
        Automatic
    }

    public abstract class FirearmWeapon : Weapon
    {
        [SerializeField] private FirearmWeaponType _type;

        private bool _canShoot;
        
        protected override void OnConstruction(GameObject owner)
        {
            base.OnConstruction(owner);
            
            _canShoot = true;
        }

        protected abstract void OnShoot(Vector3 direction);

        protected override void OnFire(Vector3 direction)
        {
            if (!_canShoot)
            {
                return;
            }
            
            if (Ammo == 0)
            {
                return;
            }
            
            OnShoot(direction);
            
            ConsumeAmmo();

            if (_type == FirearmWeaponType.Single)
            {
                _canShoot = false;
            }
        }

        protected override void OnStopFiring()
        {
            base.OnStopFiring();
            
            if (_type == FirearmWeaponType.Single)
            {
                _canShoot = true;
            }
        }
    }
}