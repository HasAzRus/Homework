using UnityEngine;

namespace Homework
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class PlayerMovement : Behaviour
    {
        [SerializeField] private float _speed;

        private Vector2 _inputDirection;
        
        private Rigidbody2D _rigidbody2d;

        protected override void Start()
        {
            base.Start();

            _rigidbody2d = GetComponent<Rigidbody2D>();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            _rigidbody2d.velocity = _inputDirection * _speed;
            _inputDirection = Vector2.zero;
        }

        public void Move(Vector2 direction)
        {
            _inputDirection = direction;
        }
    }
}