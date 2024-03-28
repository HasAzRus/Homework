using System;
using UnityEngine;

namespace Homework
{
    [RequireComponent(typeof(EnemyMovement))]
    public class Enemy : Character
    {
        [SerializeField] private float _damage;
        
        [SerializeField] private float _playerDetectionRadius;
        [SerializeField] private float _attackRadius;

        [SerializeField] private float _maxWaitingForAttackTime;

        [SerializeField] private Drops _drops;

        private bool _isAttacking;
        private bool _isWaitingForAttack;
        
        private float _waitingForAttackTime;

        private Character _target;

        private Transform _transform;
        private EnemyMovement _movement;
        
        public void Construct(Character target)
        {
            _target = target;
        }

        protected override void Start()
        {
            base.Start();

            _transform = transform;
            _movement = GetComponent<EnemyMovement>();
            
            WritableHealth.Apply(ReadableHealth.MaxValue);
        }

        private void OnDrawGizmos()
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(Vector3.zero, _playerDetectionRadius);
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(Vector3.zero, _attackRadius);
        }

        private void WaitAttack()
        {
            if (_waitingForAttackTime < _maxWaitingForAttackTime)
            {
                _waitingForAttackTime += Time.deltaTime;
            }
            else
            {
                _waitingForAttackTime -= _maxWaitingForAttackTime;
                _isWaitingForAttack = false;
            }
        }

        private bool CheckAttack(float distance)
        {
            if (!(distance < _attackRadius))
            {
                return false;
            }
            
            return !_isWaitingForAttack;
        }

        private void Attack(IDamageable damageable)
        {
            damageable.ApplyDamage(gameObject, _damage);
            
            _isWaitingForAttack = true;
        }

        protected override void Update()
        {
            base.Update();

            if (_target == null)
            {
                return;
            }

            if (_target.IsDied)
            {
                if (_movement.IsMoving)
                {
                    _movement.StopMovement();
                }
                
                return;
            }

            var position = _transform.position;
            var targetPosition = _target.transform.position;

            var distance = Vector3.Distance(position, targetPosition);

            if (distance > _playerDetectionRadius)
            {
                if (_movement.IsMoving)
                {
                    _movement.StopMovement();
                }
                
                return;
            }
            
            if (CheckAttack(distance))
            {
                Attack(_target);
            }

            if (_isWaitingForAttack)
            {
                WaitAttack();
            }

            if (!_movement.IsMoving)
            {
                _movement.StartMovement(_target.transform);
            }
        }

        protected override void OnKill(GameObject caller)
        {
            base.OnKill(caller);
            
            _drops.Drop(transform.position);
            
            Destroy(gameObject);
        }
    }
}