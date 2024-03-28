using UnityEngine;

namespace Homework
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyMovement : Behaviour
    {
        [SerializeField] private float _speed;
        
        private Transform _target;

        private Transform _transform;
        private Rigidbody2D _rigidbody2d;

        public bool IsMoving { get; private set; }

        protected override void Start()
        {
            base.Start();

            _transform = transform;
            
            _rigidbody2d = GetComponent<Rigidbody2D>();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            
            if (!IsMoving)
            {
                return;
            }

            var direction = (_target.position - _transform.position).normalized;
            _rigidbody2d.velocity = direction * _speed;
        }

        public void StartMovement(Transform target)
        {
            IsMoving = true;
            
            _target = target;
        }

        public void StopMovement()
        {
            IsMoving = false;
            
            _rigidbody2d.velocity = Vector2.zero;
        }
    }
}