using UnityEngine;

namespace Homework
{
    public class WeaponPickup : Behaviour, IInteractive
    {
        [SerializeField] private int _index;
        
        public bool Interact(IInteractable interactable)
        {
            if (!interactable.GiveWeapon(_index))
            {
                return false;
            }

            Destroy(gameObject);

            return true;
        }
    }
}