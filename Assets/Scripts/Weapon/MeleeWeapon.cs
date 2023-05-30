using System.Collections.Generic;

using UnityEngine;

using Units.Fight;


namespace Weapon
{
    public class MeleeWeapon : BasicWeapon
    {
        private readonly float AttackDistance;
        public float Distance { get => AttackDistance * 0.6f; }

        private float _targetAngle;
        private readonly float MaxAngleDifference;

        private float _delay;
        private readonly float MaxAttackDelay;
        public bool IsAttackInProgress => _delay > 0;

        public MeleeWeapon(float attackDistance, float angleOffset, float attackDelay,
            float maxCooldown, int damage)
            : base(maxCooldown, damage)
        {
            AttackDistance = attackDistance;
            MaxAngleDifference = angleOffset;
            MaxAttackDelay = attackDelay;

            OnTick = null;
        }

        public override void TryAttack(Vector2 direction)
        {
            base.TryAttack(direction);

            if (!_isReady)
                return;

            _targetAngle = Vector2.Angle(Vector2.right, direction);

            _cooldown = MaxCooldown;
            _delay = MaxAttackDelay;

            OnTick = WaitAndAttack;
        }

        private void WaitAndAttack(float deltaTime)
        {
            _delay = Mathf.Max(0, _delay - deltaTime);

            if (_delay <= 0)
            {
                DoDamage();

                OnTick = ReduceCooldown;
            }
        }

        private void DoDamage()
        {
            foreach (var currentHitbox in GetHitboxes())
                currentHitbox.Hit().TakeDamage(_damage);
        }

        private HashSet<Hitbox> GetHitboxes()
        {
            HashSet<Hitbox> result = new HashSet<Hitbox>();

            Vector2 selfPosition = _attackPoint.position;

            RaycastHit2D[] hit = Physics2D.CircleCastAll(selfPosition,
            AttackDistance, Vector2.right, 0);

            for (int i = 0; i < hit.Length; i++)
                if (isHitCorrect(hit[i], out Hitbox hitbox))
                    result.Add(hitbox);

            return result;

            bool isHitCorrect(RaycastHit2D hit, out Hitbox hitbox)
            {
                if (hit.collider.TryGetComponent(out Hitbox tempHitbox))
                    hitbox = tempHitbox;
                else
                {
                    hitbox = null;
                    return false;
                }

                if (hit.collider.CompareTag(_attackerTag))
                    return false;

                Vector2 selfPosition = _attackPoint.position;
                Vector2 direction = (hit.point - selfPosition).normalized;
                float newAngle = Vector2.Angle(Vector2.right, direction);

                float difference = Mathf.Abs(_targetAngle - newAngle) % 360;

                if (difference > 180)
                    difference = 360 - difference;

                return difference <= MaxAngleDifference;
            }
        }
    }
}