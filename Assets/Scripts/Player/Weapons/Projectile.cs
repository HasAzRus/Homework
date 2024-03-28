using UnityEngine;

namespace Homework
{
    public abstract class Projectile : Behaviour
    {
        [SerializeField] private float _maxTimeToDestroy;

        private float _timeToDestroy;
        
        private float _damage;
        private GameObject _owner;
        
        public void Construct(GameObject owner, float damage)
        {
            _owner = owner;
            _damage = damage;
        }

        protected abstract void OnLaunch(Vector3 direction);

        protected override void Update()
        {
            base.Update();

            if (_timeToDestroy < _maxTimeToDestroy)
            {
                _timeToDestroy += Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);

            if (other.gameObject == _owner)
            {
                return;
            }

            if (!other.TryGetComponent<IDamageable>(out var damageable))
            {
                return;
            }
            
            damageable.ApplyDamage(_owner, _damage);
            Destroy(gameObject);
        }

        public void Launch(Vector3 direction)
        {
            OnLaunch(direction);
        }
    }
}