using UnityEngine;


namespace Weapon
{
    [CreateAssetMenu(fileName = "MeleeWeaponSettings", menuName = "ScriptableObjects/WeaponSettings/BasicMeleeWeapon", order = 2)]
    public class MeleeWeaponSettings : ScriptableObject
    {
        [SerializeField] private float _attackDistance;
        public float AttackDistance { get => _attackDistance; }

        [SerializeField] private float _angleOffset;
        [SerializeField] private float _attackDelay;

        [Space(10)]
        [SerializeField] private float _maxCooldown;
        [SerializeField] private int _damage;

        public MeleeWeapon GetWeapon()
        {
            MeleeWeapon tempWeapon = new MeleeWeapon(_attackDistance, _angleOffset, _attackDelay, _maxCooldown, _damage);

            return tempWeapon;
        }
    }
}