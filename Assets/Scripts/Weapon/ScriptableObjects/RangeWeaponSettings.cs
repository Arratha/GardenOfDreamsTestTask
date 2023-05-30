using UnityEngine;


namespace Weapon
{
    [CreateAssetMenu(fileName = "RangeWeaponSettings", menuName = "ScriptableObjects/WeaponSettings/BasicRangeWeapon", order = 1)]
    public class RangeWeaponSettings : ScriptableObject
    {
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private float _projectileSpeed;

        [Space(10)]
        [SerializeField] private float _maxCooldown;
        [SerializeField] private int _damage;

        public IWeapon GetWeapon()
        {
            RangeWeapon tempWeapon = new RangeWeapon(_projectilePrefab, _projectileSpeed, _maxCooldown, _damage);

            return tempWeapon;
        }
    }
}