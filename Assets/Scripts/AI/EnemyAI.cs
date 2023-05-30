using System;

using UnityEngine;

using Main.Tags;


namespace AI
{
    public delegate bool Check();

    public class EnemyAI : MonoBehaviour
    {
        public enum AIState { Follow, Attack, Stay, Stunned }

        public event Action<Vector2> MoveEnemy;
        public event Action<Vector2> RotateEnemy;
        public event Action<AttackCallback> Attack;

        [SerializeField] private float _sightDistance;
        [SerializeField] private float _sightDistanceAgressive;

        [SerializeField] private AIState _currentState = AIState.Stay;
        private AIState _previousState;

        private float _stunTimer;
        private const float StunMaxTimer = 1f;

        private Vector2 _direction;

        private bool _isAgressive;

        public Check CheckForPlayerInRange;

        private void FixedUpdate()
        {
            State();
        }

        private void State()
        {
            switch (_currentState)
            {
                case AIState.Follow:

                    if (CheckForPlayerInRange != null
                        && CheckForPlayerInRange.Invoke())
                    {
                        GoAttack();
                        break;
                    }

                    if (!CheckForPlayer())
                    {
                        GoStay();
                        break;
                    }

                    MoveEnemy?.Invoke(_direction);

                    break;
                case AIState.Attack:

                    Attack?.Invoke(GoFollow);

                    break;
                case AIState.Stay:

                    if (CheckForPlayer())
                        GoFollow();

                    break;
                case AIState.Stunned:

                    _stunTimer = Mathf.Max(_stunTimer - Time.fixedDeltaTime, 0);

                    if (_stunTimer <= 0)
                        ChangeState(_previousState);

                    break;
            }
        }

        private void GoStay()
        {
            _isAgressive = false;

            ChangeState(AIState.Stay);
        }

        private void GoFollow()
        {
            _isAgressive = true;

            ChangeState(AIState.Follow);
        }

        private void GoAttack()
        {
            CheckForPlayer();
            RotateEnemy?.Invoke(_direction);

            ChangeState(AIState.Attack);
        }

        public void GoStun(float damage)
        {
            if (_currentState == AIState.Stunned)
                return;

            ChangeState(AIState.Stunned);

            _stunTimer = StunMaxTimer;
        }

        private void ChangeState(AIState state)
        {
            _previousState = _currentState;
            _currentState = state;
        }

        private bool CheckForPlayer()
        {
            float sightDistance = (_isAgressive) ? _sightDistanceAgressive : _sightDistance;
            Vector2 selfPosition = transform.position;

            RaycastHit2D[] hit = Physics2D.CircleCastAll(selfPosition,
               sightDistance, Vector2.right, 0);

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
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, _sightDistance);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _sightDistanceAgressive);
        }
    }
}