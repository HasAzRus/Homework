using UnityEngine;
using UnityEngine.UI;

namespace Homework
{
    public class UserInterface : Behaviour
    {
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private WeaponView _weaponView;
        
        [SerializeField] private Button _inventoryButton;

        public void Construct(Player player)
        {
            _inventoryView.Construct(player.GetInventory(), player);
            _weaponView.Construct(player.GetWeaponManager());
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            
            _inventoryButton.onClick.AddListener(_inventoryView.ToggleActive);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            _inventoryButton.onClick.RemoveListener(_inventoryView.ToggleActive);
        }

        public void Clear()
        {
            _inventoryView.Clear();
            _weaponView.Clear();
        }
    }
}