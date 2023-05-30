using UnityEngine;


namespace Units.Move
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BasicMovement : MonoBehaviour, IMovable
    {
        [SerializeField] private float _speed;

        private Rigidbody2D _rigidBody;

        [Space(10)]
        [Header("Optional")]
        [SerializeField] private Transform _skin;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
        }

        public void Move(Vector2 targetVector)
        {
            Vector2 movementVelocity = targetVector * _speed;
            Vector2 selfPosition = transform.position;

            _rigidBody.MovePosition(selfPosition + movementVelocity);

            if (_skin != null && targetVector.x != 0)
                Rotate(targetVector);
        }

        public void Rotate(Vector2 targetVector)
        {
            _skin.localScale = new Vector3(Mathf.Abs(_skin.localScale.x) * Mathf.Sign(targetVector.x),
                _skin.localScale.y, _skin.localScale.z);
        }


        public void JumpBack(Vector2 threatPosition, float force)
        {
            Vector2 selfPosition = transform.position;
            Vector2 targetVector = -1 * (threatPosition - selfPosition).normalized;

            _rigidBody.AddForce(targetVector * force);
        }
    }
}