using UnityEngine;

namespace Homework
{
    public class Pickup : Behaviour, IInteractive
    {
        [SerializeField] private string _name;
        [SerializeField] private int _count;
        
        public bool Interact(IInteractable interactable)
        {
            if (!interactable.GiveItem(_name, _count))
            {
                return false;
            }
            
            Destroy(gameObject);
                
            return true;
        }
    }
}