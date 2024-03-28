using UnityEngine;

namespace Homework
{
    public interface IDamageable
    {
        void ApplyDamage(GameObject caller, float amount);
    }
}