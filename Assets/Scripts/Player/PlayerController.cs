using System;

using UnityEngine;

using UI.Player;
using UI.Player.Inventory;
using Units;
using Items;


namespace Player
{
    public class PlayerController : UnitController
    {
        public event Action OnPlayerDie;

        [Space(10)]
        [SerializeField] private PlayerAttack _attackScript;

        [SerializeField] private PlayerInventory _inventoryScript;

        #region Accessors
        public PlayerAttack AttackScript { get => _attackScript; }
        public PlayerInventory InventoryScript { get => _inventoryScript; }
        #endregion

        private Vector2 _direction = Vector2.right;

        protected override void Initialize()
        {
            base.Initialize();

            JoystickController.MovePlayer += MoveSelf;
            FireButtonController.Attack += Attack;
            InventoryController.DeleteCell += _inventoryScript.RemoveItem;

            _healthScript.OnDie += Die;
        }

        private void Die()
        {
            Destroy(gameObject);

            OnPlayerDie?.Invoke();
        }

        private void MoveSelf(Vector2 targetVector)
        {
            if (targetVector != Vector2.zero)
                _direction = targetVector;

            _movementScript.Move(targetVector);
        }

        private void Attack()
        {
            _attackScript.Attack(_direction);
        }

        private void OnDestroy()
        {
            JoystickController.MovePlayer -= MoveSelf;
            FireButtonController.Attack -= Attack;
            InventoryController.DeleteCell -= _inventoryScript.RemoveItem;

            _healthScript.OnDie -= Die;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IPickable pickable))
                pickable.Pick(this);
        }
    }
}