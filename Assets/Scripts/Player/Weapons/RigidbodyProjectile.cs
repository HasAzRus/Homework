using UnityEngine;

namespace Homework
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class RigidbodyProjectile : Projectile
    {
        [SerializeField] private float _speed;
        [SerializeField] private Rigidbody2D _rigidbody2d;
        
        protected override void OnLaunch(Vector3 direction)
        {
            _rigidbody2d.AddForce(direction * _speed, ForceMode2D.Impulse);
        }
    }
}