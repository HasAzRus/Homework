namespace Homework
{
    public class FinishTrigger : Behaviour, IInteractive
    {
        public bool Interact(IInteractable interactable)
        {
            Finish.SetFinish();
            
            return true;
        }
    }
}