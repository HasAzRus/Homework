using UnityEngine;

namespace Homework
{
    public abstract class Character : Behaviour, IDamageable, IKillable
    {
        [SerializeField] private Health _health;
        
        protected IWritableHealth WritableHealth => _health;
        public IReadableHealth ReadableHealth => _health;

        public bool IsDied { get; private set; }
        
        protected virtual void OnKill(GameObject caller)
        {
            
        }

        public void ApplyDamage(GameObject caller, float amount)
        {
            _health.Remove(amount);

            if (_health.IsEmpty)
            {
                Kill(caller);
            }
        }

        public void Kill(GameObject caller)
        {
            IsDied = true;
            
            OnKill(caller);
        }
    }
}