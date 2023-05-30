using System.Linq;

using UnityEngine;

using Units;
using Items;


namespace AI
{
    public class EnemyController : UnitController
    {
        [Space(10)]
        [SerializeField] private EnemyAI _ai;
        [SerializeField] private EnemyAttack _attackScript;

        protected override void Initialize()
        {
            base.Initialize();

            _ai.MoveEnemy += _movementScript.Move;
            _ai.RotateEnemy += _movementScript.Rotate;
            _ai.Attack += _attackScript.Attack;
            _ai.CheckForPlayerInRange = _attackScript.CheckForPlayerInRange;

            _healthScript.OnTakeDamage += _ai.GoStun;
            _healthScript.OnDie += Die;
        }

        private void Die()
        {
            DropItem();
            Destroy(gameObject);
        }

        private void DropItem()
        {
            BasicItem item = ItemsController.Instance.Items.ElementAt(Random.Range(0, ItemsController.Instance.Items.Count)).Value;

            Instantiate(item.Prefab, transform.position, new Quaternion(0, 0, 0, 0));
        }

        private void OnDestroy()
        {
            _ai.MoveEnemy -= _movementScript.Move;
            _ai.RotateEnemy -= _movementScript.Rotate;
            _ai.Attack -= _attackScript.Attack;
            _ai.CheckForPlayerInRange = null;

            _healthScript.OnTakeDamage -= _ai.GoStun;
            _healthScript.OnDie -= Die;
        }
    }
}