using UnityEngine;

namespace Homework
{
    public sealed class PlayerInteraction : Behaviour
    {
        private IInteractable _interactable;
        
        public void Construct(IInteractable interactable)
        {
            _interactable = interactable;
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);

            if (!other.TryGetComponent<IInteractive>(out var interactive))
            {
                return;
            }

            interactive.Interact(_interactable);
        }
    }
}