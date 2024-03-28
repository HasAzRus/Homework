using UnityEngine;

namespace Homework
{
    public class AmmoPickup : Behaviour, IInteractive
    {
        [SerializeField] private string _name;
        [SerializeField] private int _amount;
        
        public bool Interact(IInteractable interactable)
        {
            if (!interactable.GiveAmmo(_name, _amount))
            {
                return false;
            }
            
            Destroy(gameObject);

            return true;
        }
    }
}