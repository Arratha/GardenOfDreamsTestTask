using Main.Tags;
using System.Collections.Generic;
using Units.Fight;
using UnityEngine;
using Weapon;


namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private Transform _attackPoint;

        [Space(10)]
        [SerializeField] private float _sightDistance;

        private List<IWeapon> _weapons = new List<IWeapon>();

        private void FixedUpdate()
        {
            foreach (var currentWeapon in _weapons)
                currentWeapon.OnTick?.Invoke(Time.fixedDeltaTime);
        }

        public void AddWeapon(IWeapon weapon)
        {
            weapon.SetAttackPoint(_attackPoint);
            weapon.SetTag(Tags.Player);

            _weapons.Add(weapon);
        }

        public void Attack(Vector2 direction)
        {
            if (_weapons.Count == 0)
                return;

            TryFindEnemy(ref direction);

            _weapons[0].TryAttack(direction);
        }

        private void TryFindEnemy(ref Vector2 direction)
        {
            Vector2 selfPosition = _attackPoint.position;

            RaycastHit2D[] hit = Physics2D.CircleCastAll(selfPosition,
             _sightDistance, Vector2.right, 0);

            int index = -1;
            float distance = Mathf.Infinity;

            for (int i = 0; i < hit.Length; i++)
                if (hit[i].collider.CompareTag(Tags.Enemy)
                    && hit[i].collider.TryGetComponent(out Hitbox hitbox))
                    if (Vector2.Distance(selfPosition, hit[i].point) < distance)
                    {
                        distance = Vector2.Distance(selfPosition, hit[i].point);
                        index = i;
                    }

            if (index != -1)
            {
                Vector2 enemyPosition = hit[index].collider.transform.position;
                direction = (enemyPosition - selfPosition).normalized;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _sightDistance);
        }
    }
}