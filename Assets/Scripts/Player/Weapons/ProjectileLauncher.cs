using UnityEngine;

namespace Homework
{
    public abstract class ProjectileLauncher : FirearmWeapon
    {
        [SerializeField] private Projectile _projectilePrefab;

        protected override void OnShoot(Vector3 direction)
        {
            var ownerTransform = Owner.transform;
            
            var projectile = Instantiate(_projectilePrefab, ownerTransform.position,
                Quaternion.LookRotation(Vector3.forward, direction));

            projectile.Construct(Owner, Damage);
            projectile.Launch(direction);
        }
    }
}