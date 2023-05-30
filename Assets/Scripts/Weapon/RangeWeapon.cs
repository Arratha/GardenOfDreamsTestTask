using UnityEngine;


namespace Weapon
{
    public class RangeWeapon : BasicWeapon
    {
        private Projectile _projectilePrefab;
        protected float _projectileSpeed;

        public RangeWeapon(Projectile projectilePrefab, float projectileSpeed,
           float maxCooldown, int damage)
            : base(maxCooldown, damage)
        {
            _projectilePrefab = projectilePrefab;
            _projectileSpeed = projectileSpeed;
        }

        public override void TryAttack(Vector2 direction)
        {
            if (!_isReady)
                return;

            Projectile projectile = Object.Instantiate(_projectilePrefab, _attackPoint.position, new Quaternion(0, 0, 0, 0));
            projectile.Initialize(_attackerTag, direction * _projectileSpeed, _damage);

            _cooldown = MaxCooldown;
        }
    }
}