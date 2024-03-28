using UnityEngine;

namespace Homework
{
    public interface IControllable
    {
        void Move(Vector2 direction);
        void Shoot();
        void StopShooting();
    }
}