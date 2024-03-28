using System;
using UnityEngine;

namespace Homework
{
    public abstract class Weapon : Behaviour, IReadableWeapon
    {
        public event Action<int> AmmoChanged;
        
        [SerializeField] private string _name;
        [SerializeField] private bool _isActive;
        
        [SerializeField] private float _damage;
        [SerializeField] private float _maxCooldownTime;
        
        [SerializeField] private int _maxAmmo;
        [SerializeField] private string _ammoName;

        private float _cooldownTime;
        private bool _isCooldown;
        
        public string Name => _name;
        public bool IsActive => _isActive;
        
        public int MaxAmmo => _maxAmmo;
        public int Ammo { get; private set; }
        
        public float Damage => _damage;

        public string AmmoName => _ammoName;
        
        public GameObject Owner { get; private set; }
        
        public void Construct(GameObject owner)
        {
            Owner = owner;

            OnConstruction(owner);
        }

        protected virtual void OnConstruction(GameObject owner)
        {
            
        }

        private void SetAmmo(int value)
        {
            Ammo = value;
            
            AmmoChanged?.Invoke(value);
        }

        protected void ConsumeAmmo()
        {
            var value = Ammo - 1;

            SetAmmo(value > 0 ? value : 0);
        }

        protected abstract void OnFire(Vector3 direction);

        protected virtual void OnStopFiring()
        {
            
        }

        protected override void Update()
        {
            base.Update();

            if (!_isCooldown)
            {
                return;
            }
            
            if (_cooldownTime < _maxCooldownTime)
            {
                _cooldownTime += Time.deltaTime;
            }
            else
            {
                _cooldownTime -= _maxCooldownTime;
                    
                _isCooldown = false;
            }
        }

        public void Fire(Vector3 direction)
        {
            if (!_isActive)
            {
                return;
            }
            
            if (_isCooldown)
            {
                return;
            }
            
            OnFire(direction);
            
            _isCooldown = true;
        }

        public void StopFiring()
        {
            OnStopFiring();
        }
        
        public bool Reload(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("Значение amount ниже нуля");
            }
            
            if (Ammo == MaxAmmo)
            {
                return false;
            }

            var value = Ammo + amount;

            SetAmmo(value < MaxAmmo ? value : MaxAmmo);

            return true;
        }
    }
}