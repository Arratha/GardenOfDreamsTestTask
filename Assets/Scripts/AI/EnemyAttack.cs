using UnityEngine;

using Weapon;
using Main.Tags;


namespace AI
{
    public delegate void AttackCallback();
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private Transform _attackPoint;

        [Space(10)]
        [SerializeField] private MeleeWeaponSettings _weaponObject;

        private MeleeWeapon _weapon;

        private Vector2 _direction;

        private void Awake()
        {
            AddWeapon(_weaponObject.GetWeapon());
        }

        private void FixedUpdate()
        {
           _weapon.OnTick?.Invoke(Time.fixedDeltaTime);
        }

        private void AddWeapon(MeleeWeapon weapon)
        {
            weapon.SetAttackPoint(_attackPoint);
            weapon.SetTag(Tags.Enemy);

            _weapon = weapon;
        }

        public void Attack(AttackCallback callback)
        {
            if (!_weapon.IsAttackInProgress)
                callback.Invoke();

            _weapon.TryAttack(_direction);
        }

        public bool CheckForPlayerInRange()
        {
            Vector2 selfPosition = _attackPoint.position;

            RaycastHit2D[] hit = Physics2D.CircleCastAll(selfPosition,
             _weapon.Distance, Vector2.right, 0);

            int index = -1;
            float distance = Mathf.Infinity;

            for (int i = 0; i < hit.Length; i++)
                if (hit[i].collider.CompareTag(Tags.Player))
                    if (Vector2.Distance(selfPosition, hit[i].point) < distance)
                    {
                        distance = Vector2.Distance(selfPosition, hit[i].point);
                        index = i;
                    }

            if (index != -1)
                _direction = (hit[index].point - selfPosition).normalized;
            
            return index != -1;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(_attackPoint.position, _weaponObject.AttackDistance);
        }
    }
}