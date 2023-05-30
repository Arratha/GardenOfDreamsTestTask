using System;
using UnityEngine;


namespace Weapon
{
    public abstract class BasicWeapon : IWeapon
    {
        public Action<float> OnTick { get; protected set; }

        protected Transform _attackPoint;
        protected string _attackerTag;

        protected float _cooldown;
        protected readonly float MaxCooldown;

        protected int _damage;

        protected bool _isReady => _cooldown <= 0;


        public BasicWeapon(float maxCooldown, int damage)
        {
            MaxCooldown = maxCooldown;
            _damage = damage;

            OnTick = ReduceCooldown;
        }

        public virtual void TryAttack(Vector2 direction)
        {
            CheckComponents();
        }

        public void ReduceCooldown(float deltaTime)
        {
            _cooldown = Mathf.Max(0, _cooldown - deltaTime);
        }

        public void SetAttackPoint(Transform attackPoint)
        {
            _attackPoint = attackPoint;
        }

        public void SetTag(string attackerTag)
        {
            _attackerTag = attackerTag;
        }

        private void CheckComponents()
        {
            if (_attackPoint == null)
                throw new Exception("No attack point");

            if (string.IsNullOrEmpty(_attackerTag))
                throw new Exception("No attacker tag");
        }
    }
}