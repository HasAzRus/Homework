using UnityEngine;

namespace Homework
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerInteraction))]
    [RequireComponent(typeof(PlayerEnemyFinder))]
    [RequireComponent(typeof(PlayerWeaponManager))]
    [RequireComponent(typeof(PlayerDataSaveLoad))]
    public class Player : Character, IControllable, IInteractable
    {
        [SerializeField] private int _maxInventoryCapacity;

        private Inventory _inventory;
        
        private PlayerMovement _movement;
        private PlayerInteraction _interaction;
        private PlayerEnemyFinder _enemyFinder;
        private PlayerWeaponManager _weaponManager;
        private PlayerDataSaveLoad _saveLoad;

        private Transform _transform;

        protected override void Awake()
        {
            base.Awake();

            _inventory = new Inventory(_maxInventoryCapacity);
        }

        protected override void Start()
        {
            base.Start();

            _transform = transform;
            
            _movement = GetComponent<PlayerMovement>();
            _interaction = GetComponent<PlayerInteraction>();
            _enemyFinder = GetComponent<PlayerEnemyFinder>();
            
            _weaponManager = GetComponent<PlayerWeaponManager>();

            _saveLoad = GetComponent<PlayerDataSaveLoad>();

            _interaction.Construct(this);
            _weaponManager.Construct(gameObject, this);
        }
        
        public void Move(Vector2 direction)
        {
            if (IsDied)
            {
                return;
            }
            
            _movement.Move(direction);
        }

        public void Shoot()
        {
            if (IsDied)
            {
                return;
            }
            
            if (!_enemyFinder.CheckEnemies())
            {
                return;
            }

            if (_enemyFinder.NearestEnemy == null)
            {
                return;
            }

            var nearestEnemy = _enemyFinder.NearestEnemy;
            var direction = (nearestEnemy.position - _transform.position).normalized;

            _weaponManager.CurrentWeapon.Fire(direction);
        }

        public void StopShooting()
        {
            if (_weaponManager.CurrentWeapon == null)
            {
                return;
            }
            
            _weaponManager.CurrentWeapon.StopFiring();
        }

        public bool GiveItem(string name, int count)
        {
            return _inventory.Place(name, count);
        }

        public bool RemoveItem(string name, int count)
        {
            return _inventory.Remove(name, count);
        }

        public bool RemoveItem(string name)
        {
            return _inventory.Remove(name);
        }

        public bool GiveAmmo(string name, int amount)
        {
            if (_weaponManager.CurrentWeapon == null)
            {
                return _inventory.Place(name, 1);
            }

            return _weaponManager.CurrentWeapon.Reload(amount) || _inventory.Place(name, 1);
        }

        public bool GiveWeapon(int index)
        {
            if (!_weaponManager.ChangeWeapon(index))
            {
                return false;
            }

            var currentWeapon = _weaponManager.CurrentWeapon;

            if (_inventory.Remove(currentWeapon.AmmoName, 1))
            {
                currentWeapon.Reload(currentWeapon.MaxAmmo);
            }

            return true;
        }

        public void Save(IWritableData saveData)
        {
            var data = new PlayerData()
            {
                Health = ReadableHealth.Value,
                CurrentWeaponIndex = _weaponManager.CurrentWeaponIndex,
                Ammo = _weaponManager.CurrentWeapon != null ? _weaponManager.CurrentWeapon.Ammo : 0,  
                Items = _inventory.GetItems()
            };
                
            _saveLoad.Save(saveData, data);
        }

        public void Load(IReadableData readData)
        {
            var playerData = _saveLoad.Load(readData);
            
            WritableHealth.Apply(playerData.Health);

            if (playerData.CurrentWeaponIndex != -1)
            {
                _weaponManager.ChangeWeapon(playerData.CurrentWeaponIndex);
                _weaponManager.CurrentWeapon.Reload(playerData.Ammo);
            }

            if (playerData.Items.Length > 0)
            {
                foreach (var item in playerData.Items)
                {
                    _inventory.Place(item.Name, item.Count);
                }
            }
        }

        public IReadableInventory GetInventory()
        {
            return _inventory;
        }

        public IReadableWeaponManager GetWeaponManager()
        {
            return _weaponManager;
        }
    }
}