using UnityEngine;

using Units.Move;
using Units.Fight;


namespace Units
{
    [RequireComponent(typeof(IMovable))]
    public abstract class UnitController : MonoBehaviour
    {
        [SerializeField] protected Health _healthScript;
        public Health HealthScript { get => _healthScript; }

        [SerializeField] private Hitbox _hitboxScript;

        protected IMovable _movementScript;

        private void Awake()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            _movementScript = GetComponent<IMovable>();

            _hitboxScript.Initialize(this);
        }

        public void TakeDamage(int damage)
        {
            _healthScript.TakeDamage(damage);
        }

        public void TakeDamage(int damage, Vector2 threatPosition, float force)
        {
            _movementScript.JumpBack(threatPosition, force);

            _healthScript.TakeDamage(damage);
        }
    }
}