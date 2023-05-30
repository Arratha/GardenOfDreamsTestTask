using System;

using UnityEngine;


namespace Weapon
{
    public interface IWeapon
    {
        public void TryAttack(Vector2 direction);
        public void SetAttackPoint(Transform attackPoint);
        public void SetTag(string attackerTag);

        public Action<float> OnTick { get; }
    }
}