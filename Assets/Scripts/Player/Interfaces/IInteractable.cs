namespace Homework
{
    public interface IInteractable
    {
        bool GiveItem(string name, int count);
        bool RemoveItem(string name, int count);
        bool RemoveItem(string name);
        bool GiveAmmo(string name, int amount);
        bool GiveWeapon(int index);
    }
}